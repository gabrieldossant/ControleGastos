using System.ComponentModel.DataAnnotations;

namespace ControleGastos.Models
{
    public class PessoaModel
    {
        [Key]
        public int PessoaId { get; set; }
        [MaxLength(200)]
        public string Nome { get; set; } = string.Empty;
        public int Idade { get; set; }
    }
}
