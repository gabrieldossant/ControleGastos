using ControleGastos.Data;
using ControleGastos.Interfaces;
using ControleGastos.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleGastos.Repositories
{
    public class PessoaRepository : IPessoaRepository
    {
        private readonly AppDbContext _context;
        public PessoaRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<PessoaModel>> GetAllPessoas()
        {
            return await _context.Pessoas
                .Include(x => x.Transacoes)
                .ToListAsync();
        }
        public async Task<PessoaModel?> GetById(int id)
        {
            return await _context.Pessoas
                .Include(x => x.Transacoes)
                .FirstOrDefaultAsync(x => x.PessoaId == id);
        }
        public async Task Add(PessoaModel pessoa)
        {
            await _context.Pessoas.AddAsync(pessoa);
            await _context.SaveChangesAsync();
        }
        public async Task Update(PessoaModel pessoa)
        {
            _context.Pessoas.Update(pessoa);
            await _context.SaveChangesAsync();
        }
        public async Task Delete(PessoaModel pessoa)
        {
            _context.Pessoas.Remove(pessoa);
            await _context.SaveChangesAsync();  
        }
    }
}
