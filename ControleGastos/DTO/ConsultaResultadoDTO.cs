namespace ControleGastos.DTO
{
    public class ConsultaResultadoDTO
    {
        public List<ConsultaPorPessoaDTO> Pessoas { get; set; } = new List<ConsultaPorPessoaDTO>();

        public decimal TotalReceitas => Pessoas.Sum(p => p.TotalReceitas);
        public decimal TotalDespesas => Pessoas.Sum(p => p.TotalDespesas);
        public decimal SaldoGeral => TotalReceitas - TotalDespesas;
    }
}
