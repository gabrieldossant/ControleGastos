using ControleGastos.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControleGastos.Models
{
    public class TransacaoModel
    {
        [Key]
        public int TransacaoId { get; set; }
        [MaxLength(400)]
        [Required]
        public string Descricao { get; set; } = string.Empty;
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Valor { get; set; }
        [Required]
        public TipoTransacao Tipo { get; set; }
        public DateTime Data { get; set; } = DateTime.Now;

        // FK Categoria
        [ForeignKey("Categoria")]
        public int CategoriaId { get; set; }
        public CategoriaModel Categoria { get; set; } = null!;

        // FK Pessoa
        [ForeignKey("Pessoa")]
        public int PessoaId { get; set; }
        public PessoaModel Pessoa { get; set; } = null!;
    }
}
