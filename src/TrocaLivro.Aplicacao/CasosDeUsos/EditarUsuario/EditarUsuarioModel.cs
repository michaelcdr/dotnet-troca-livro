using System.ComponentModel.DataAnnotations;

namespace TrocaLivro.Aplicacao.CasosDeUsos
{
    public class EditarUsuarioModel
    {
        [Display(Name = "Login")]
        [Required(ErrorMessage = "Informe o campo {0}")]
        public string Login { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Informe o campo {0}")]
        public string Nome { get; set; }

        [Display(Name = "Sobrenome")]
        [Required(ErrorMessage = "Informe o campo {0}")]
        public string Sobrenome { get; set; }

        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "Informe o campo {0}")]
        public string Email { get; set; }

        public string Avatar { get; set; }
    }
}
