using System.ComponentModel.DataAnnotations;

namespace TrocaLivro.Aplicacao.CasosDeUsos
{
    public class LogarUsuarioModel
    {
        [Required(ErrorMessage = "Informe o {0}")]
        [Display(Name = "Usuário")]
        public string Usuario { get; set; }

        [Display(Name = "Senha")]
        [Required(ErrorMessage = "Informe a {0}")]
        public string Senha { get; set; }
        public LogarUsuarioModel()
        {

        }
        public LogarUsuarioModel(string usuario, string senha)
        {
            this.Usuario = usuario; 
            this.Senha = senha;
        }
    }
}
