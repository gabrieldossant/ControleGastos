using ControleGastos.Models;

namespace ControleGastos.Interfaces
{
    public interface ICategoriaRepository
    {
        Task<List<CategoriaModel>> GetCategoriaAll();
        Task<CategoriaModel?> GetById(int id);
        Task Add(CategoriaModel categoria);
        Task Update(CategoriaModel categoria);
        Task Delete(CategoriaModel categoria);
    }
}
