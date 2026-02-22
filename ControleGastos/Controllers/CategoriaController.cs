using ControleGastos.DTOs.Categoria;
using ControleGastos.DTOs.Pessoa;
using ControleGastos.Models;
using ControleGastos.Response;
using ControleGastos.Services;
using Microsoft.AspNetCore.Mvc;

namespace ControleGastos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriaController : Controller
    {
        private readonly CategoriaService _categoriaService;
        public CategoriaController(CategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var categorias = await _categoriaService.Listar();
            return Ok(categorias);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CriarCategoriaDTO categoriaDto)
        {
            var resultado = await _categoriaService.Criar(categoriaDto);
            return Ok(ApiResponse<CategoriaResponseDTO>.Success(resultado, "Categoria criada com sucesso. "));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] EditarCategoriaDTO categoriaDto)
        {
            var resultado = await _categoriaService.Atualizar(id, categoriaDto);
            return Ok(ApiResponse<CategoriaResponseDTO>.Success(resultado, "Categoria editada com sucesso. "));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _categoriaService.Deletar(id);
            return NoContent();
        }
    }
}
