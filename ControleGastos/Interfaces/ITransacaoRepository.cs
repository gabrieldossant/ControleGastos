using ControleGastos.DTOs.Transacao;
using ControleGastos.Models;

namespace ControleGastos.Interfaces
{
    public interface ITransacaoRepository
    {
        Task<PessoaModel?> GetPessoa(CriarTransacaoDTO pessoa);
        Task<CategoriaModel?> GetCategoria(CriarTransacaoDTO categoria);
        Task<List<TransacaoModel>> GetAllAsync();
        Task<TransacaoModel?> GetTransacaoById(int id);
        Task<decimal> GetSaldoAsync();
        Task AddAsync(TransacaoModel transacao);
        Task Update(TransacaoModel transacao);
        Task Delete(TransacaoModel transacao);
    }
}
