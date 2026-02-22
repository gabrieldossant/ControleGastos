using ControleGastos.DTOs.Pessoa;
using ControleGastos.Models;
using ControleGastos.Response;
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
        public async Task<IActionResult> Post([FromBody] CriarPessoaDTO pessoaDto)
        {
            var resultado = await _pessoaService.CriarAsync(pessoaDto);
            return Ok(ApiResponse<PessoaResponseDTO>.Success(resultado, "Pessoa criada com sucesso. "));
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] CriarPessoaDTO pessoaDto)
        {
            var pessoaModel = new PessoaModel()
            {
                PessoaId = id,
                Nome = pessoaDto.Nome,
                Idade = pessoaDto.Idade
            };

            await _pessoaService.Atualizar(id, pessoaModel);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _pessoaService.Deletar(id);
            return NoContent();
        }

        [HttpGet("totais")]
        public async Task<IActionResult> ConsultarTotaisPorPessoa()
        {
            var resultado = await _pessoaService.ObterTotalPessoa();
            return Ok(resultado);
        }
    }
}
