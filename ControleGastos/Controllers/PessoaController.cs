using ControleGastos.DTOs.Pessoa;
using ControleGastos.Models;
using ControleGastos.Response;
using ControleGastos.Services;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace ControleGastos.Controllers
{
    /// <summary>
    /// Controller responsável pelo gerenciamento de pessoas.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class PessoaController : Controller
    {
        /// <summary>
        /// Construtor responsável pelo gerenciamento de pessoas e acesso ao service. 
        /// </summary>
        private readonly PessoaService _pessoaService;
        public PessoaController(PessoaService pessoaService)
        {
            _pessoaService = pessoaService;
        }

        /// <summary>
        /// Lista todas as pessoas cadastradas no sistema.  
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var pessoas = await _pessoaService.ListarPessoas();
            return Ok(pessoas);
        }

        /// <summary>
        /// Busca uma pessoa específica pelo seu ID. Retorna os detalhes da pessoa, incluindo suas despesas associadas.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var pessoa = await _pessoaService.BuscarPorId(id);

            if (pessoa == null)
                return NotFound();

            return Ok(pessoa);
        }

        /// <summary>
        /// Cria uma nova pessoa no sistema. Recebe os dados da pessoa a ser criada e retorna os detalhes da pessoa criada, incluindo seu ID gerado.
        /// </summary>
        /// <param name="pessoaDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CriarPessoaDTO pessoaDto)
        {
            var resultado = await _pessoaService.CriarAsync(pessoaDto);
            return Ok(ApiResponse<PessoaResponseDTO>.Success(resultado, "Pessoa criada com sucesso. "));
        }

        /// <summary>
        /// Atualiza os dados de uma pessoa existente. Recebe o ID da pessoa a ser atualizada e os novos dados, e retorna os detalhes da pessoa atualizada.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pessoaDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] EditarPessoaDTO pessoaDto)
        {
            var resultado = await _pessoaService.Atualizar(id, pessoaDto);
            return Ok(ApiResponse<PessoaResponseDTO>.Success(resultado, "Dados alterado com sucesso. "));
        }

        /// <summary>
        /// Deletar uma pessoa do sistema. Recebe o ID da pessoa a ser deletada e remove a pessoa do banco de dados. Retorna uma resposta indicando o sucesso da operação.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _pessoaService.Deletar(id);
            return NoContent();
        }

        /// <summary>
        /// Busca os totais de receitas, despesas e saldo para cada pessoa. 
        /// Retorna uma lista de objetos contendo o nome da pessoa, total de receitas, total de despesas e saldo.
        /// </summary>
        /// <returns></returns>
        [HttpGet("totais")]
        public async Task<IActionResult> ConsultarTotaisPorPessoa()
        {
            var resultado = await _pessoaService.ObterTotalPessoa();
            return Ok(resultado);
        }
    }
}
