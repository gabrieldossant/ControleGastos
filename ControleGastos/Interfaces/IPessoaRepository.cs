using ControleGastos.Models;

namespace ControleGastos.Interfaces
{
    /// <summary>
    /// Interface responsável pelo contrato de operações relacionadas à entidade Pessoa, 
    /// incluindo métodos para obter todas as pessoas, obter por ID, adicionar, atualizar e excluir pessoas.
    /// </summary>
    public interface IPessoaRepository
    {
        Task<List<PessoaModel>> GetAllPessoas();
        Task<PessoaModel?> GetById(int id);
        Task Add(PessoaModel pessoa);
        Task Update(PessoaModel pessoa);
        Task Delete(PessoaModel pessoa);
    }
}
