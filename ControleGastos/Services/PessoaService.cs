using ControleGastos.DTO;
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
        public async Task<List<PessoaModel>> ListarPessoas()
        {
            return await _pessoaRepository.GetAllPessoas();
        }
        public async Task<PessoaModel?> BuscarPorId(int id)
        {
            return await _pessoaRepository.GetById(id);
        }
        public async Task CriarAsync(PessoaModel pessoa)
        {
            await _pessoaRepository.Add(pessoa);
        }
        public async Task Atualizar(int id, PessoaModel pessoaAlterada)
        {
            var pessoa = await _pessoaRepository.GetById(id);

            if (pessoa == null)
                throw new Exception($"Não foi encontrado {pessoaAlterada.Nome} em nosso banco de dados.");

            pessoa.Nome = pessoaAlterada.Nome;
            pessoa.Idade = pessoaAlterada.Idade;

            await _pessoaRepository.Update(pessoa);
        }
        public async Task Deletar(int id)
        {
            var deletarPessoa = await _pessoaRepository.GetById(id);

            if (deletarPessoa == null)
                throw new Exception("Não foi encontrado essa pessoa em nosso banco de dados.");

            await _pessoaRepository.Delete(deletarPessoa);
        }
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
