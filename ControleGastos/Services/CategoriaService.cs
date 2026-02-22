using ControleGastos.DTOs.Categoria;
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

        public async Task<List<CategoriaResponseDTO>> Listar()
        {
            var categoriasModel = await _categoriaRepository.GetCategoriaAll();

            return categoriasModel.Select(x => new CategoriaResponseDTO
            {
                CategoriaId = x.CategoriaId,
                Descricao = x.Descricao,
                Finalidade = x.Finalidade
            }).ToList();
        }

        public async Task<CategoriaResponseDTO> Criar(CriarCategoriaDTO categoriaDto)
        {
            var categoriaModel = new CategoriaModel()
            {
                Descricao = categoriaDto.Descricao,
                Finalidade = categoriaDto.Finalidade
            };
            await _categoriaRepository.Add(categoriaModel);

            return new CategoriaResponseDTO()
            {
                CategoriaId = categoriaModel.CategoriaId,
                Descricao = categoriaModel.Descricao,
                Finalidade = categoriaModel.Finalidade
            };
        }

        public async Task<CategoriaResponseDTO> Atualizar(int id, EditarCategoriaDTO categoriaAlterada)
        {
            var categoriaModel = await _categoriaRepository.GetById(id);

            if (categoriaModel == null)
                throw new Exception("Categoria não encontrada");

            categoriaModel.Descricao = categoriaAlterada.Descricao;
            categoriaModel.Finalidade = categoriaAlterada.Finalidade;

            await _categoriaRepository.Update(categoriaModel);

            return new CategoriaResponseDTO()
            {
                CategoriaId = categoriaModel.CategoriaId,
                Descricao = categoriaModel.Descricao,
                Finalidade = categoriaModel.Finalidade
            };
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
