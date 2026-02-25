using ControleGastos.DTOs.Transacao;
using ControleGastos.Models;

namespace ControleGastos.Interfaces
{
    /// <summary>
    /// Interface que define o contrato para operações relacionadas à entidade Transação, incluindo métodos para obter transações, obter por ID, adicionar, atualizar e excluir transações, 
    /// bem como métodos específicos para obter o saldo atual e validar a existência de pessoas e categorias
    /// </summary>
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
