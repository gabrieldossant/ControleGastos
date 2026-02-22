using System.ComponentModel.DataAnnotations;

namespace ControleGastos.DTOs.Pessoa
{
    public class EditarPessoaDTO
    {
        [Required(ErrorMessage = "Este campo é obrigatório.")]
        public int PessoaId { get; set; }
        [MaxLength(200)]
        [Required(ErrorMessage = "Este campo é obrigatório.")]
        public string Nome { get; set; }
        [Range(1, 120, ErrorMessage = "Idade inválida")]
        [Required(ErrorMessage = "Este campo é obrigatório.")]
        public int Idade { get; set; }
    }
}
