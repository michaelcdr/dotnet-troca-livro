using System.ComponentModel.DataAnnotations;

namespace TrocaLivro.Aplicacao.CasosDeUsos
{
    public class RegistrarModel
    {
        [Required(ErrorMessage = "Informe o {0}")]
        public string Usuario { get; set; }

        [Required(ErrorMessage = "Informe a {0}")]
        public string Senha { get; set; }
        
        [Required(ErrorMessage = "Informe o {0}")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Confirme a {0}")]
        public string ConfirmarSenha { get; set; }
    }
}
