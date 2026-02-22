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
