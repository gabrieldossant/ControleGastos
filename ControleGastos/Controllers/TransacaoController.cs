using ControleGastos.Models;
using ControleGastos.Services;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace ControleGastos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransacaoController : ControllerBase
    {
        private readonly TransacaoService _service;
        public TransacaoController(TransacaoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _service.ListarAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Post(TransacaoModel transacao)
        {
            try
            {
                await _service.CriarAsync(transacao);
                return Ok("Transação criada com sucesso.");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] TransacaoModel transacao)
        {
            await _service.Atualizar(id, transacao);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.Deletar(id);
            return NoContent();
        }

        [HttpGet("saldo")]
        public async Task<IActionResult> GetSaldo()
        {
            return Ok(await _service.ObterSaldoAsync());
        }
    }
}
