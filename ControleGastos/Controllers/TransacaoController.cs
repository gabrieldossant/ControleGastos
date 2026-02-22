using ControleGastos.Models;
using ControleGastos.Services;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _service.ListarAsync());
        }

        [HttpGet("saldo")]
        public async Task<IActionResult> GetSaldo()
        {
            return Ok(await _service.ObterSaldoAsync());
        }
    }
}
