using ControleGastos.Enum;
using ControleGastos.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControleGastos.DTOs.Transacao
{
    public class TransacaoResponseDTO
    {
        public int TransacaoId { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public TipoTransacao Tipo { get; set; }
        public DateTime Data { get; set; } 

        public int CategoriaId { get; set; }
        public string NomeCategoria { get; set; }

        public int PessoaId { get; set; }
        public string NomePessoa { get; set; }
    }
}
