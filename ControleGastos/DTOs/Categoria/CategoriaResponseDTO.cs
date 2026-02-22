using ControleGastos.Enum;

namespace ControleGastos.DTOs.Categoria
{
    public class CategoriaResponseDTO
    {
        public int CategoriaId { get; set; }
        public string Descricao { get; set; } 
        public FinalidadeCategoria Finalidade { get; set; }
    }
}
