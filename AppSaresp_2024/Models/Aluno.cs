using System.ComponentModel.DataAnnotations;

namespace AppSaresp_2024.Models
{
    public class Aluno
    {
        [Display(Name = "Código")]
        public int? IdAluno { get; set; }

        [Display(Name = "Nome Completo")]
        [Required(ErrorMessage = "O campo nome é obrigatório")]
        public string NomeAluno { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "O campo email é obrigatório")]
        public string Email { get; set; }

        [Display(Name = "Telefone")]
        [Required(ErrorMessage = "O campo telefone é obrigatório")]
        public decimal TelefoneAluno { get; set; }

        [Display(Name = "Serie")]
        [Required(ErrorMessage = "O campo serie é obrigatório")]
        public string Serie { get; set; }

        [Display(Name = "Turma")]
        [Required(ErrorMessage = "O campo turma é obrigatório")]
        public string Turma { get; set; }

        [Display(Name = "Nascimento")]
        [Required(ErrorMessage = "O campo nascimento é obrigatório")]
        [DataType(DataType.DateTime)]
        public DateTime DataNascimentoAluno { get; set; }
    }
}
