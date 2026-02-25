using ControleGastos.DTOs.Categoria;
using ControleGastos.DTOs.Pessoa;
using ControleGastos.Models;
using ControleGastos.Response;
using ControleGastos.Services;
using Microsoft.AspNetCore.Mvc;

namespace ControleGastos.Controllers
{
    /// <summary>
    /// Controller Categoria responsável pelo gerenciamento de categorias.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriaController : Controller
    {
        private readonly CategoriaService _categoriaService;
        public CategoriaController(CategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        /// <summary>
        /// Lista todas as categorias cadastradas no sistema.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var categorias = await _categoriaService.Listar();
            return Ok(categorias);
        }

        /// <summary>
        /// Cria uma nova categoria no sistema. Recebe um objeto do tipo CriarCategoriaDTO contendo as informações da categoria a ser criada. 
        /// Retorna um objeto do tipo CategoriaResponseDTO com os detalhes da categoria criada.
        /// </summary>
        /// <param name="categoriaDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CriarCategoriaDTO categoriaDto)
        {
            var resultado = await _categoriaService.Criar(categoriaDto);
            return Ok(ApiResponse<CategoriaResponseDTO>.Success(resultado, "Categoria criada com sucesso. "));
        }

        /// <summary>
        /// Atualiza uma categoria existente no sistema. 
        /// Recebe o ID da categoria a ser atualizada e um objeto do tipo EditarCategoriaDTO contendo as novas informações da categoria.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="categoriaDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] EditarCategoriaDTO categoriaDto)
        {
            var resultado = await _categoriaService.Atualizar(id, categoriaDto);
            return Ok(ApiResponse<CategoriaResponseDTO>.Success(resultado, "Categoria editada com sucesso. "));
        }

        /// <summary>
        /// Deleta uma categoria existente no sistema. Recebe o ID da categoria a ser deletada e remove a categoria correspondente do banco de dados. 
        /// Retorna uma resposta de sucesso sem conteúdo (204 No Content) para indicar que a operação foi concluída com êxito.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _categoriaService.Deletar(id);
            return NoContent();
        }
    }
}
