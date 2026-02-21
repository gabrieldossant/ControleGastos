using System.ComponentModel.DataAnnotations;

namespace ControleGastos.Models
{
    public class PessoaModel
    {
        [Key]
        public int PessoaId { get; set; }
        [MaxLength(200)]
        [Required]
        public string Nome { get; set; } = string.Empty;
        [Required]
        public int Idade { get; set; }
        
        public List<TransacaoModel> Transacoes { get; set; } = new();
    }
}
