namespace ControleGastos.DTO
{
    public class ConsultaPorPessoaDTO
    {
        public string Nome { get; set; }
        public decimal TotalReceitas { get; set; }
        public decimal TotalDespesas { get; set; }
        public decimal Saldo => TotalReceitas - TotalDespesas;
    }
}
