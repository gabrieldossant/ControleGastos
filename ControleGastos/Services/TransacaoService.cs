using ControleGastos.Enum;
using ControleGastos.Interfaces;
using ControleGastos.Models;

namespace ControleGastos.Services
{
    public class TransacaoService
    {
        private readonly ITransacaoRepository _repository;
        public TransacaoService(ITransacaoRepository repository)
        {
            _repository = repository;
        }

        public async Task CriarAsync(TransacaoModel transacao)
        {
            var pessoa = await _repository.GetPessoa(transacao);

            if (pessoa == null)
                throw new Exception("Pessoa não encontrada no banco de dados.");

            var categoria = await _repository.GetCategoria(transacao);

            if (categoria == null)
                throw new Exception("Categoria não encontrada no banco de dados");

            if (pessoa.Idade < 18 && transacao.Tipo == TipoTransacao.Receita)
                throw new Exception("Menor de idade não pode receber receitas apenas despesas.");

            if (transacao.Tipo == TipoTransacao.Despesa &&
                categoria.Finalidade == FinalidadeCategoria.Receita)
                throw new Exception("Categoria não permite Despesa.");

            if (transacao.Valor <= 0)
                throw new Exception("Valor deve ser maior que zero.");

            transacao.Data = DateTime.Now;

            await _repository.AddAsync(transacao);
        }
        public async Task<List<TransacaoModel>> ListarAsync()
        {
            return await _repository.GetAllAsync();
        }
        public async Task<decimal> ObterSaldoAsync()
        {
            return await _repository.GetSaldoAsync();
        }
    }
}
