using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace TrocaLivro.Dominio.Entidades
{
    public class Usuario : IdentityUser
    {
        public string Nome { get; set; }
        private List<string> _erros { get; set; }

        public Usuario(string nome, string userName, string email)
        {
            this.Nome = nome;
            this.UserName = userName;
            this.Email = email;
        }

        public bool TaValido()
        {
            bool retorno = true;

            this._erros = new List<string>();

            if (string.IsNullOrEmpty(Nome))
            {
                _erros.Add("Nome não informado.");
                retorno = false;
            }

            if (string.IsNullOrEmpty(UserName))
            {
                _erros.Add("UserName não informado.");
                retorno = false;
            }

            return retorno;
        }

        public List<string> ObterErros()
        {
            return this._erros;
        }
    }
}
