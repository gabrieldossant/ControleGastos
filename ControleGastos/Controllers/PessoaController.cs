using ControleGastos.Services;
using Microsoft.AspNetCore.Mvc;

namespace ControleGastos.Controllers
{
    public class PessoaController : Controller
    {
        private readonly PessoaService _pessoaService;
        public PessoaController(PessoaService pessoaService)
        {
            _pessoaService = pessoaService;
        }
        
        [HttpPost]
        public async Task<IActionResult> CriarPessoa()
        {
            
        }
    }
}
