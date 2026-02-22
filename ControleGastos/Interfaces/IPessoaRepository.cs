using ControleGastos.Models;

namespace ControleGastos.Interfaces
{
    public interface IPessoaRepository
    {
        Task<List<PessoaModel>> GetAllPessoas();
        Task<PessoaModel?> GetById(int id);
        Task Add(PessoaModel pessoa);
        Task Update(PessoaModel pessoa);
        Task Delete(PessoaModel pessoa);
    }
}
