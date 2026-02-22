using ControleGastos.DTOs.Categoria;
using ControleGastos.DTOs.Transacao;
using ControleGastos.Models;
using ControleGastos.Response;
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
        public async Task<IActionResult> Post(CriarTransacaoDTO transacaoDto)
        {
            try
            {
                var transacaoModel = await _service.CriarAsync(transacaoDto);
                return Ok(ApiResponse<TransacaoResponseDTO>.Success(transacaoModel, "Transação realizada com sucesso. "));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] EditarTransacaoDTO transacao)
        {
            var transacaoModel = await _service.Atualizar(id, transacao);
            return Ok(ApiResponse<TransacaoResponseDTO>.Success(transacaoModel, "Transação editada com sucesso. "));
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
