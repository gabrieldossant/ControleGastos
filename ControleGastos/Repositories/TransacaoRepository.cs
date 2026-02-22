using ControleGastos.Data;
using ControleGastos.Enum;
using ControleGastos.Interfaces;
using ControleGastos.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleGastos.Repositories
{
    public class TransacaoRepository : ITransacaoRepository
    {
        private readonly AppDbContext _context;
        public TransacaoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(TransacaoModel transacao)
        {
            await _context.Transacoes.AddAsync(transacao);
            await _context.SaveChangesAsync();
        }

        public async Task<List<TransacaoModel>> GetAllAsync()
        {
            return await _context.Transacoes
                .OrderByDescending(x => x.TransacaoId)
                .ToListAsync();
        }

        public async Task<PessoaModel?> GetPessoa(TransacaoModel pessoa)
        {
            return await _context.Pessoas
                 .FirstOrDefaultAsync(x => x.PessoaId == pessoa.PessoaId);
        }

        public async Task<CategoriaModel?> GetCategoria(TransacaoModel categoria)
        {
            return await _context.Categorias
                .FirstOrDefaultAsync(x => x.CategoriaId == categoria.CategoriaId);
        }

        public async Task<decimal> GetSaldoAsync()
        {
            var entradas = await _context.Transacoes
            .Where(t => t.Tipo == TipoTransacao.Receita)
            .SumAsync(t => t.Valor);

            var saidas = await _context.Transacoes
                .Where(t => t.Tipo == TipoTransacao.Despesa)
                .SumAsync(t => t.Valor);

            return entradas - saidas;
        }
    }
}
