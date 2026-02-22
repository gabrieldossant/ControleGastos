using ControleGastos.Models;
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
        public async Task<IActionResult> Post([FromBody] CategoriaModel categoria)
        {
            await _categoriaService.Criar(categoria);
            return Created("", categoria);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] CategoriaModel categoria)
        {
            await _categoriaService.Atualizar(id, categoria);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _categoriaService.Deletar(id);
            return NoContent();
        }
    }
}
