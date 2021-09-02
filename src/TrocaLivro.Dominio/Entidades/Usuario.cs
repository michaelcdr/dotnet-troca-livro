using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace TrocaLivro.Dominio.Entidades
{
    public class Usuario : IdentityUser
    {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        private List<Notificacao> _erros { get; set; }

        public Usuario(string nome, string userName, string email)
        {
            this.Nome = nome;
            this.UserName = userName;
            this.Email = email;
        }

        public bool TaValido()
        {
            bool retorno = true;

            this._erros = new List<Notificacao>();

            if (string.IsNullOrEmpty(Nome))
            {
                _erros.Add(new Notificacao("Nome não informado.","Nome"));
                retorno = false;
            }

            if (string.IsNullOrEmpty(UserName))
            {
                _erros.Add(new Notificacao("UserName não informado.",""));
                retorno = false;
            }

            return retorno;
        }

        public List<Notificacao> ObterErros()
        {
            return this._erros;
        }
    }
}
