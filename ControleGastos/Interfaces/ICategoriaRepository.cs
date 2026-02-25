using ControleGastos.Models;

namespace ControleGastos.Interfaces
{
    /// <summary>
    /// Interface responsavel pelo contrato de operações relacionadas à entidade Categoria, incluindo métodos para obter todas as categorias, 
    /// obter por ID, adicionar, atualizar e excluir categorias.
    /// </summary>
    public interface ICategoriaRepository
    {
        Task<List<CategoriaModel>> GetCategoriaAll();
        Task<CategoriaModel?> GetById(int id);
        Task Add(CategoriaModel categoria);
        Task Update(CategoriaModel categoria);
        Task Delete(CategoriaModel categoria);
    }
}
