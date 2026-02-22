using ControleGastos.Enum;
using System.ComponentModel.DataAnnotations;

namespace ControleGastos.DTOs.Transacao
{
    public class CriarTransacaoDTO
    {
        [MaxLength(400)]
        [Required(ErrorMessage = "Este campo é obrigatório.")]
        public string Descricao { get; set; } = string.Empty;
        [Range(0.01, double.MaxValue)]
        [Required(ErrorMessage = "Este campo é obrigatório.")]
        public decimal Valor { get; set; }
        [Required(ErrorMessage = "Este campo é obrigatório.")]
        public TipoTransacao Tipo { get; set; }
        public DateTime Data { get; set; }

        [Required]
        public int PessoaId { get; set; }

        [Required]
        public int CategoriaId { get; set; }
    }
}
