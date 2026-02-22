using System.ComponentModel.DataAnnotations;

namespace ControleGastos.Models
{
    public class PessoaModel
    {
        [Key]
        public int PessoaId { get; set; }
        [MaxLength(200)]
        [Required(ErrorMessage = "Este campo é obrigatório.")]
        public string Nome { get; set; } = string.Empty;
        [Required(ErrorMessage = "Este campo é obrigatório.")]
        public int Idade { get; set; }
        
        public List<TransacaoModel> Transacoes { get; set; } = new();
    }
}
