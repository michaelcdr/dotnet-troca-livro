using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using TrocaLivro.Dominio;

namespace TrocaLivro.Aplicacao.Helpers
{
    public class IdentityHelper
    {
        public static List<Notificacao> ObterErros(IdentityResult identityResult)
        {
            var erros = new List<Notificacao>();

            foreach (var item in identityResult.Errors)
                if ("PasswordMismatch" == item.Code)
                    erros.Add(new Notificacao("As senhas não conferem.",""));
                else if ("DuplicateUserName" == item.Code)
                    erros.Add(new Notificacao("O usuário informado está em uso.",""));
                else
                    erros.Add(new Notificacao($"{item.Code} - {item.Description}",""));

            return erros;
        }
    }
}
