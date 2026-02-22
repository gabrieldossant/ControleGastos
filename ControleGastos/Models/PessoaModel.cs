using System.ComponentModel.DataAnnotations;

namespace ControleGastos.Models
{
    public class PessoaModel
    {
        [Key]
        public int PessoaId { get; set; }
        [MaxLength(200)]
        public string Nome { get; set; } = string.Empty;
        [Range(1, 120)]
        public int Idade { get; set; }
        
        public List<TransacaoModel> Transacoes { get; set; } = new();
    }
}
