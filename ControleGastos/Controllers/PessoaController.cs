using ControleGastos.Models;
using ControleGastos.Services;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace ControleGastos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PessoaController : Controller
    {
        private readonly PessoaService _pessoaService;
        public PessoaController(PessoaService pessoaService)
        {
            _pessoaService = pessoaService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var pessoas = await _pessoaService.ListarPessoas();
            return Ok(pessoas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var pessoa = await _pessoaService.BuscarPorId(id);

            if (pessoa == null)
                return NotFound();

            return Ok(pessoa);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PessoaModel pessoa)
        {
            await _pessoaService.CriarAsync(pessoa);
            return Created("", pessoa);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] PessoaModel pessoa)
        {
            await _pessoaService.Atualizar(id, pessoa);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _pessoaService.Deletar(id);
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> ConsultarTotaisPorPessoa()
        {
            var resultado = await _pessoaService.ObterTotalPessoa();
            return Ok(resultado);
        }
    }
}
