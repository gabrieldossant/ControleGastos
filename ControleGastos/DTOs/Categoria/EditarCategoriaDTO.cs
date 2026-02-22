using ControleGastos.Enum;
using System.ComponentModel.DataAnnotations;

namespace ControleGastos.DTOs.Categoria
{
    public class EditarCategoriaDTO
    {
        [Required]
        public int CategoriaId { get; set; }
        [Required]
        [MaxLength(400)]
        public string Descricao { get; set; }
        [Required]
        public FinalidadeCategoria Finalidade { get; set; }
    }
}
