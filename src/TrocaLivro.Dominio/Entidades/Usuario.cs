using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace TrocaLivro.Dominio.Entidades
{
    public class Usuario : IdentityUser
    {
        public string Nome { get; private set; }
        public string Sobrenome { get; private set; }
        public int Pontos { get; private set; }
        public string Avatar { get; private set; }
        private List<Notificacao> _erros { get; set; }
        public List<LivroDisponibilizadoParaTroca> Trocas { get; set; }
        public Usuario(string nome, string userName, string email, string sobrenome)
        {
            this.Nome = nome;
            this.UserName = userName;
            this.Email = email;
            this.Sobrenome = sobrenome;
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
