using ControleGastos.DTOs.Transacao;
using ControleGastos.Enum;
using ControleGastos.Interfaces;
using ControleGastos.Models;
using ControleGastos.Repositories;
using System.Drawing;

namespace ControleGastos.Services
{
    /// <summary>
    /// Service responsável pelo gerenciamento de transações, incluindo operações de criação, leitura, atualização e exclusão (CRUD) de transações,
    /// </summary>
    public class TransacaoService
    {
        private readonly ITransacaoRepository _transacaoRepository;
        public TransacaoService(ITransacaoRepository transacaoRepository)
        {
            _transacaoRepository = transacaoRepository;
        }

        /// <summary>
        /// Cria uma nova transação, realizando validações para garantir a integridade dos dados, como verificar se a pessoa e a categoria existem, 
        /// se o menor de idade pode receber receitas, se a categoria permite o tipo de transação e se o valor é positivo.
        /// </summary>
        /// <param name="transacaoDto"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<TransacaoResponseDTO> CriarAsync(CriarTransacaoDTO transacaoDto)
        {
            var pessoa = await _transacaoRepository.GetPessoa(transacaoDto);

            if (pessoa == null)
                throw new Exception("Pessoa não encontrada no banco de dados.");

            var categoria = await _transacaoRepository.GetCategoria(transacaoDto);

            if (categoria == null)
                throw new Exception("Categoria não encontrada no banco de dados");

            if (pessoa.Idade < 18 && transacaoDto.Tipo == TipoTransacao.Receita)
                throw new Exception("Menor de idade não pode receber receitas apenas despesas.");

            if (transacaoDto.Tipo == TipoTransacao.Despesa &&
                categoria.Finalidade == FinalidadeCategoria.Receita)
                throw new Exception("Categoria não permite Despesa.");

            if (transacaoDto.Valor <= 0)
                throw new Exception("Valor deve ser maior que zero.");

            var transacaoModel = new TransacaoModel()
            {
                Descricao = transacaoDto.Descricao,
                Valor = transacaoDto.Valor,
                Tipo = transacaoDto.Tipo,
                Data = DateTime.Now,
                CategoriaId = transacaoDto.CategoriaId,
                PessoaId = transacaoDto.PessoaId
            };

            await _transacaoRepository.AddAsync(transacaoModel);

            return new TransacaoResponseDTO()
            {
                TransacaoId = transacaoModel.TransacaoId,
                Descricao = transacaoModel.Descricao,
                Valor = transacaoModel.Valor,
                Tipo = transacaoModel.Tipo,
                Data = transacaoModel.Data,
                CategoriaId = transacaoModel.CategoriaId,
                NomeCategoria = transacaoModel.Categoria.Descricao,
                PessoaId = transacaoModel.PessoaId,
                NomePessoa = transacaoModel.Pessoa.Nome
            };
        }

        public async Task<List<TransacaoResponseDTO>> ListarAsync()
        {
            var transacaoModel = await _transacaoRepository.GetAllAsync();

            return transacaoModel.Select(x => new TransacaoResponseDTO {
                TransacaoId = x.TransacaoId,
                Descricao = x.Descricao,
                Valor = x.Valor,
                Tipo = x.Tipo,
                Data = x.Data,
                CategoriaId = x.CategoriaId,
                NomeCategoria = x.Categoria.Descricao,
                PessoaId = x.PessoaId,
                NomePessoa = x.Pessoa.Nome
            }).ToList();
        }

        // Atualiza uma transação existente, permitindo a modificação de seus atributos,
        // como descrição, valor e tipo, e garantindo que a transação exista antes de realizar a atualização.
        public async Task<TransacaoResponseDTO> Atualizar(int id, EditarTransacaoDTO transacaoAlterada)
        {
            var transacaoModel = await _transacaoRepository.GetTransacaoById(id);

            if (transacaoModel == null)
                throw new Exception("Transação não encontrada");

            transacaoModel.Descricao = transacaoAlterada.Descricao;
            transacaoModel.Valor = transacaoAlterada.Valor;
            transacaoModel.Tipo = transacaoAlterada.Tipo;

            await _transacaoRepository.Update(transacaoModel);

            return new TransacaoResponseDTO()
            {
                TransacaoId = transacaoModel.TransacaoId,
                Descricao = transacaoModel.Descricao,
                Valor = transacaoModel.Valor,
                Tipo = transacaoModel.Tipo,
                CategoriaId = transacaoModel.CategoriaId,
                NomeCategoria = transacaoModel.Categoria.Descricao,
                PessoaId = transacaoModel.PessoaId,
                NomePessoa = transacaoModel.Pessoa.Nome
            };
        }

        /// <summary>
        /// Deleta uma transação existente, removendo-a do banco de dados, e caso a transação não seja encontrada, lança uma exceção para indicar que a operação não pode ser concluída.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task Deletar (int id)
        {
            var transacao = await _transacaoRepository.GetTransacaoById(id);

            if (transacao == null)
                throw new Exception("Transação não encontrada");

            await _transacaoRepository.Delete(transacao);
        }

        /// <summary>
        /// Retorna o saldo atual, calculado como a diferença entre o total de receitas e o total de despesas, utilizando os dados das transações registradas no sistema.
        /// </summary>
        /// <returns></returns>
        public async Task<decimal> ObterSaldoAsync()
        {
            return await _transacaoRepository.GetSaldoAsync();
        }
    }
}
