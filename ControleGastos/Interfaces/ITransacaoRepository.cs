using ControleGastos.Models;

namespace ControleGastos.Interfaces
{
    public interface ITransacaoRepository
    {
        Task<PessoaModel?> GetPessoa(TransacaoModel pessoa);
        Task<CategoriaModel?> GetCategoria(TransacaoModel categoria);
        Task AddAsync(TransacaoModel transacao);
        Task<List<TransacaoModel>> GetAllAsync();
        Task<decimal> GetSaldoAsync();
    }
}
