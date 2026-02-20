using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControleGastos.Models
{
    public class TransacaoModel
    {
        [Key]
        public int TransacaoId { get; set; }
        [MaxLength(400)]
        public string Descricao { get; set; } = string.Empty;
        public double Valor { get; set; }
        public string Tipo { get; set; } = string.Empty;
        public int CategoriaId { get; set; }
        public CategoriaModel? Categoria { get; set; }
        public int PessoaId { get; set; }
        public PessoaModel? Pessoa { get; set;  }
    }
}
