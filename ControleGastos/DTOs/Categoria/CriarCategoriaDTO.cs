using ControleGastos.Enum;
using System.ComponentModel.DataAnnotations;

namespace ControleGastos.DTOs.Categoria
{
    public class CriarCategoriaDTO
    {
        [MaxLength(400)]
        [Required(ErrorMessage = "Campo 'Descrição' obrigatório.")]
        public string Descricao { get; set; } = string.Empty;
        [Required(ErrorMessage = "Campo 'Finalidade' obrigatório.")]
        public FinalidadeCategoria Finalidade { get; set; }
    }
}
