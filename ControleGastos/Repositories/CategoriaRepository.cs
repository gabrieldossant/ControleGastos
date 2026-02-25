using ControleGastos.Data;
using ControleGastos.Interfaces;
using ControleGastos.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleGastos.Repositories
{
    /// <summary>
    /// Repository responsável pelo gerenciamento de categorias, incluindo operações de criação, 
    /// leitura, atualização e exclusão (CRUD) de categorias, e acesso ao banco.
    /// </summary>
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly AppDbContext _context;
        public CategoriaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<CategoriaModel>> GetCategoriaAll()
        {
            return await _context.Categorias
                .Include(x => x.Transacoes)
                .ToListAsync();
        }

        public async Task<CategoriaModel?> GetById(int id)
        {
            return await _context.Categorias
                .Include(x => x.Transacoes)
                .FirstOrDefaultAsync(x => x.CategoriaId == id);
        }

        public async Task Add(CategoriaModel categoria)
        {
            await _context.Categorias.AddAsync(categoria);
            await _context.SaveChangesAsync();
        }

        public async Task Update(CategoriaModel categoria)
        {
            _context.Categorias.Update(categoria);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(CategoriaModel categoria)
        {
            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();
        }
    }
}
