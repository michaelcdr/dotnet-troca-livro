using System.ComponentModel.DataAnnotations;

namespace TrocaLivro.Aplicacao.CasosDeUsos
{
    public class RegistrarUsuarioModel
    {
        [Required(ErrorMessage = "Informe o {0}")]
        [Display(Name = "Usuário")]
        public string Usuario { get; set; }
        
        [Required(ErrorMessage = "Informe o {0}")]
        public string Nome { get; set; }

        [Required(ErrorMessage ="Informe o {0}")]
        [StringLength(100, ErrorMessage = "Máximo 100 caracteres")]
        public string Sobrenome { get; set; }

        [Required(ErrorMessage = "Informe o {0}")]
        [EmailAddress(ErrorMessage = "E-mail inválido")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Informe a {0}")]
        public string Senha { get; set; }

        [Required(ErrorMessage = "Confirme a {0}")]
        [Display(Name = "Confirmar senha")]
        [Compare("Senha",ErrorMessage = "A confirmação deve ser igual a senha.")]
        public string ConfirmarSenha { get; set; }
    }
}
