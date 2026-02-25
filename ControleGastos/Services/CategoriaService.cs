using ControleGastos.DTOs.Categoria;
using ControleGastos.Interfaces;
using ControleGastos.Models;

namespace ControleGastos.Services
{
    /// <summary>
    /// Service responsável pelo gerenciamento de categorias, incluindo operações de criação, leitura, atualização e exclusão (CRUD) de categorias.
    /// </summary>
    public class CategoriaService
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriaService(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        /// <summary>
        /// Lista todas as categorias disponíveis, retornando uma lista de objetos CategoriaResponseDTO que contêm as informações relevantes de cada categoria.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Cria uma nova categoria com base nas informações fornecidas em um objeto CriarCategoriaDTO. O método converte o DTO para um modelo de categoria, 
        /// salva a nova categoria no repositório e retorna um objeto CategoriaResponseDTO contendo os detalhes da categoria criada.
        /// </summary>
        /// <param name="categoriaDto"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Atualiza uma categoria existente identificada pelo ID fornecido, 
        /// utilizando as informações contidas em um objeto EditarCategoriaDTO. O método busca a categoria no repositório,
        /// </summary>
        /// <param name="id"></param>
        /// <param name="categoriaAlterada"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
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

        /// <summary>
        /// Deleta uma categoria existente identificada pelo ID fornecido. O método busca a categoria no repositório e, se encontrada, a remove do banco de dados.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task Deletar(int id)
        {
            var categoria = await _categoriaRepository.GetById(id);

            if (categoria == null)
                throw new Exception("Categoria não encontrada");

            await _categoriaRepository.Delete(categoria);
        }
    }
}
