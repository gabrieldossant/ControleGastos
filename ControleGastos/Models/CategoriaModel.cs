using System.ComponentModel.DataAnnotations;

namespace ControleGastos.Models
{
    public class CategoriaModel
    {
        public int CategoriaId { get; set; }
        [MaxLength(400)]
        public string Descricao { get; set; } = string.Empty;
        public string Finalidade { get; set; } = string.Empty;
    }
}
