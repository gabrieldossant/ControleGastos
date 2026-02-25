using ControleGastos.DTOs.Categoria;
using ControleGastos.DTOs.Transacao;
using ControleGastos.Models;
using ControleGastos.Response;
using ControleGastos.Services;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace ControleGastos.Controllers
{
    /// <summary>
    /// Controller Transacao responsável pelo gerenciamento de transações, incluindo operações de criação, leitura, 
    /// atualização e exclusão (CRUD) de transações, bem como a obtenção do saldo atual.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class TransacaoController : ControllerBase
    {
        private readonly TransacaoService _service;
        public TransacaoController(TransacaoService service)
        {
            _service = service;
        }

        /// <summary>
        /// Lista todas as transações cadastradas no sistema, retornando uma resposta estruturada com os dados das transações e uma mensagem de sucesso.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var transacaoModel = await _service.ListarAsync();
            return Ok(ApiResponse<List<TransacaoResponseDTO>>.Success(transacaoModel));
        }

        /// <summary>
        /// Cria uma nova transação com base nos dados fornecidos no DTO de criação, retornando um DTO de resposta com os dados da transação criada e uma mensagem de sucesso. 
        /// Em caso de erro, retorna uma resposta de erro com a mensagem correspondente.
        /// </summary>
        /// <param name="transacaoDto"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Atualiza uma transação existente com base no ID fornecido e nos dados de edição fornecidos no DTO de edição, 
        /// retornando um DTO de resposta com os dados da transação atualizada e uma mensagem de sucesso.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="transacao"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] EditarTransacaoDTO transacao)
        {
            var transacaoModel = await _service.Atualizar(id, transacao);
            return Ok(ApiResponse<TransacaoResponseDTO>.Success(transacaoModel, "Transação editada com sucesso. "));
        }

        /// <summary>
        /// Deleta uma transação existente com base no ID fornecido, removendo a transação correspondente do banco de dados.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.Deletar(id);
            return NoContent();
        }

        /// <summary>
        ///  Busca o saldo atual, calculado com base nas transações registradas, e retorna o valor do saldo em uma resposta de sucesso.
        /// </summary>
        /// <returns></returns>
        [HttpGet("saldo")]
        public async Task<IActionResult> GetSaldo()
        {
            return Ok(await _service.ObterSaldoAsync());
        }
    }
}
