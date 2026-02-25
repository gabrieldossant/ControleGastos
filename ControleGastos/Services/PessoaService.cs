using ControleGastos.DTO;
using ControleGastos.DTOs.Pessoa;
using ControleGastos.Enum;
using ControleGastos.Interfaces;
using ControleGastos.Models;

namespace ControleGastos.Services
{
    public class PessoaService
    {
        private readonly IPessoaRepository _pessoaRepository;
        public PessoaService(IPessoaRepository pessoaRepository)
        {
            _pessoaRepository = pessoaRepository;
        }
        /// <summary>
        /// Recupera de forma assíncrona todas as pessoas do repositório como uma lista de DTOs de resposta.
        /// </summary>
        /// <remarks>
        /// Utilize este método para obter a lista completa de pessoas atualmente armazenadas no repositório.
        /// A lista retornada estará vazia caso nenhum registro seja encontrado.
        /// </remarks>
        /// <returns>
        /// Uma tarefa que representa a operação assíncrona. O resultado da tarefa contém
        /// uma lista de <see cref="PessoaResponseDTO"/>, cada uma representando
        /// uma pessoa com seu identificador, nome e idade.
        /// </returns>
        public async Task<List<PessoaResponseDTO>> ListarPessoas()
        {
            var pessoaModel = await _pessoaRepository.GetAllPessoas();

            return pessoaModel.Select(x => new PessoaResponseDTO
            {
                PessoaId = x.PessoaId,
                Nome = x.Nome,
                Idade = x.Idade
            }).ToList();
        }
        /// <summary>
        /// Busca de forma assíncrona uma pessoa pelo seu identificador único (ID) e retorna um DTO de resposta contendo as informações da pessoa.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<PessoaResponseDTO?> BuscarPorId(int id)
        {
            var pessoaModel = await _pessoaRepository.GetById(id);

            return new PessoaResponseDTO()
            {
                PessoaId = pessoaModel.PessoaId,
                Nome = pessoaModel.Nome,
                Idade = pessoaModel.Idade
            };
        }

        /// <summary>
        /// Cria uma nova pessoa no repositório de forma assíncrona, utilizando os dados fornecidos em um DTO de criação, 
        /// e retorna um DTO de resposta com as informações da pessoa criada.
        /// </summary>
        /// <param name="pessoa"></param>
        /// <returns></returns>
        public async Task<PessoaResponseDTO> CriarAsync(CriarPessoaDTO pessoa)
        {
            var pessoaModel = new PessoaModel()
            {
                Nome = pessoa.Nome,
                Idade = pessoa.Idade
            };

            await _pessoaRepository.Add(pessoaModel);

            return new PessoaResponseDTO
            {
                PessoaId = pessoaModel.PessoaId,
                Nome = pessoaModel.Nome,
                Idade = pessoaModel.Idade
            };
        }

        /// <summary>
        /// Atualiza de forma assíncrona as informações de uma pessoa existente no repositório, utilizando o identificador único (ID) para localizar a pessoa e um DTO de edição contendo os novos dados. 
        /// Retorna um DTO de resposta com as informações atualizadas da pessoa.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pessoaAlterada"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<PessoaResponseDTO> Atualizar(int id, EditarPessoaDTO pessoaAlterada)
        {
            var pessoaModel = await _pessoaRepository.GetById(id);

            if (pessoaModel == null)
                throw new Exception($"Não foi encontrado {pessoaAlterada.Nome} em nosso banco de dados.");

            pessoaModel.Nome = pessoaAlterada.Nome;
            pessoaModel.Idade = pessoaAlterada.Idade;

            await _pessoaRepository.Update(pessoaModel);

            return new PessoaResponseDTO()
            {
                PessoaId = pessoaModel.PessoaId,
                Nome = pessoaModel.Nome,
                Idade = pessoaModel.Idade
            };
        }

        /// <summary>
        /// Deleta de forma assíncrona uma pessoa do repositório, utilizando o identificador único (ID) para localizar a pessoa a ser removida. Retorna uma tarefa que representa a operação de exclusão. 
        /// Caso a pessoa não seja encontrada, uma exceção é lançada indicando que a pessoa não existe no banco de dados.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task Deletar(int id)
        {
            var deletarPessoa = await _pessoaRepository.GetById(id);

            if (deletarPessoa == null)
                throw new Exception("Não foi encontrado essa pessoa em nosso banco de dados.");

            await _pessoaRepository.Delete(deletarPessoa);
        }

        /// <summary>
        /// Retorna o total de receitas, despesas e saldo para cada pessoa cadastrada no repositório.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<ConsultaResultadoDTO> ObterTotalPessoa()
        {
            var listaDePessoas = await _pessoaRepository.GetAllPessoas();

            if (listaDePessoas == null)
                throw new Exception("Não foi encontrado nenhuma pessoa cadastrada.");

            var listaTotaisPorPessoa = listaDePessoas.Select(x => new ConsultaPorPessoaDTO
            {
                Nome = x.Nome,

                TotalReceitas = x.Transacoes
                    .Where(p => p.Tipo == TipoTransacao.Receita)
                    .Sum(p => p.Valor),

                TotalDespesas = x.Transacoes
                    .Where(p => p.Tipo == TipoTransacao.Despesa)
                    .Sum(p => p.Valor)

            }).ToList();

            return new ConsultaResultadoDTO
            {
                Pessoas = listaTotaisPorPessoa
            };
        }
    }
}
