using ControleGastos.Enum;
using ControleGastos.Interfaces;
using ControleGastos.Models;
using ControleGastos.Repositories;

namespace ControleGastos.Services
{
    public class TransacaoService
    {
        private readonly ITransacaoRepository _transacaoRepository;
        public TransacaoService(ITransacaoRepository transacaoRepository)
        {
            _transacaoRepository = transacaoRepository;
        }

        public async Task CriarAsync(TransacaoModel transacao)
        {
            var pessoa = await _transacaoRepository.GetPessoa(transacao);

            if (pessoa == null)
                throw new Exception("Pessoa não encontrada no banco de dados.");

            var categoria = await _transacaoRepository.GetCategoria(transacao);

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

            await _transacaoRepository.AddAsync(transacao);
        }

        public async Task<List<TransacaoModel>> ListarAsync()
        {
            return await _transacaoRepository.GetAllAsync();
        }

        public async Task Atualizar(int id, TransacaoModel transacaoAlterada)
        {
            var transacao = await _transacaoRepository.GetTransacaoById(id);

            if (transacao == null)
                throw new Exception("Transação não encontrada");

            transacao.Descricao = transacaoAlterada.Descricao;
            transacao.Valor = transacaoAlterada.Valor;
            transacao.Tipo = transacaoAlterada.Tipo;

            await _transacaoRepository.Update(transacao);
        }
        public async Task Deletar (int id)
        {
            var transacao = await _transacaoRepository.GetTransacaoById(id);

            if (transacao == null)
                throw new Exception("Transação não encontrada");

            await _transacaoRepository.Delete(transacao);
        }

        public async Task<decimal> ObterSaldoAsync()
        {
            return await _transacaoRepository.GetSaldoAsync();
        }
    }
}
