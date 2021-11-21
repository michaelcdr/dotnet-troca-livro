using System.ComponentModel.DataAnnotations;
using TrocaLivro.Aplicacao.Helpers;

namespace TrocaLivro.Aplicacao.CasosDeUsos
{
    public class EditarUsuarioModel
    {
        public string UsuarioId { get; set; }
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

        public string ObterAvatarComCaminho() 
        {
            

            return AppHelper.ObterDiretorioAvatar(this.Avatar);
        }
    }
}
