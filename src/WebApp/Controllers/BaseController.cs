using Microsoft.AspNetCore.Mvc;
using System.Linq;
using TrocaLivro.Aplicacao.HttpClients;

namespace WebApp.Controllers
{
    public class BaseController:Controller
    {
        protected void AtualizarToken(IAtualizadorToken api)
        {
            if (HttpContext.User.Identity != null && HttpContext.User.Identity.IsAuthenticated)
                api.AtualizarToken(HttpContext.User.Claims.FirstOrDefault(e => e.Type == "Token"));
        }
    }
}
