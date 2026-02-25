using ControleGastos.Enum;
using System.ComponentModel.DataAnnotations;

namespace ControleGastos.Models
{
    /// <summary>
    /// Modelo associado ao contexto de categoria que representa as categorias de transações financeiras, 
    /// contendo informações como descrição, finalidade e a lista de transações associadas a cada categoria.
    /// </summary>
    public class CategoriaModel
    {
        [Key]
        public int CategoriaId { get; set; }
        [MaxLength(400)]
        public string Descricao { get; set; } = string.Empty;
        public FinalidadeCategoria Finalidade { get; set; }

        public List<TransacaoModel> Transacoes { get; set; } = new();
    }
}
