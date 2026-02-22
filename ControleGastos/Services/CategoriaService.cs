using ControleGastos.Interfaces;
using ControleGastos.Models;

namespace ControleGastos.Services
{
    public class CategoriaService
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriaService(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        public async Task<List<CategoriaModel>> Listar()
        {
            return await _categoriaRepository.GetCategoriaAll();
        }

        public async Task Criar(CategoriaModel categoria)
        {
            await _categoriaRepository.Add(categoria);
        }

        public async Task Atualizar(int id, CategoriaModel novaCategoria)
        {
            var categoria = await _categoriaRepository.GetById(id);

            if (categoria == null)
                throw new Exception("Categoria não encontrada");

            categoria.Descricao = novaCategoria.Descricao;
            categoria.Finalidade = novaCategoria.Finalidade;

            await _categoriaRepository.Update(categoria);
        }

        public async Task Deletar(int id)
        {
            var categoria = await _categoriaRepository.GetById(id);

            if (categoria == null)
                throw new Exception("Categoria não encontrada");

            await _categoriaRepository.Delete(categoria);
        }
    }
}
