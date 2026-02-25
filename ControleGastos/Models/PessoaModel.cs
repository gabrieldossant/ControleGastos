using System.ComponentModel.DataAnnotations;

namespace ControleGastos.Models
{
    /// <summary>
    /// Classe Modelo Pessoa associada ao contexto de pessoa, representando as pessoas envolvidas nas transações financeiras, 
    /// contendo informações como nome, idade e a lista de transações associadas a cada pessoa.
    /// </summary>
    public class PessoaModel
    {
        [Key]
        public int PessoaId { get; set; }
        [MaxLength(200)]
        public string Nome { get; set; } = string.Empty;
        [Range(1, 120)]
        public int Idade { get; set; }
        
        public List<TransacaoModel> Transacoes { get; set; } = new();
    }
}
