using ControleGastos.DTOs.Transacao;
using ControleGastos.Enum;
using ControleGastos.Interfaces;
using ControleGastos.Models;
using ControleGastos.Repositories;
using System.Drawing;

namespace ControleGastos.Services
{
    public class TransacaoService
    {
        private readonly ITransacaoRepository _transacaoRepository;
        public TransacaoService(ITransacaoRepository transacaoRepository)
        {
            _transacaoRepository = transacaoRepository;
        }

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
        public async Task Deletar (int id)
        {
            var transacao = await _transacaoRepository.GetTransacaoById(id);

            if (transacao == null)
                throw new Exception("Transação não encontrada");

            await _transacaoRepository.Delete(transacao);
        }

        public async Task<decimal> ObterSaldoAsync()
        {
            return await _transacaoRepository.GetSaldoAsync();
        }
    }
}
