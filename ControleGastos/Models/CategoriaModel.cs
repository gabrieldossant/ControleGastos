using ControleGastos.Enum;
using System.ComponentModel.DataAnnotations;

namespace ControleGastos.Models
{
    public class CategoriaModel
    {
        [Key]
        public int CategoriaId { get; set; }
        [MaxLength(400)]
        [Required]
        public string Descricao { get; set; } = string.Empty;
        [Required]
        public FinalidadeCategoria Finalidade { get; set; }

        public List<TransacaoModel> Transacoes { get; set; } = new();
    }
}
