using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using TrocaLivro.Aplicacao.CasosDeUsos;
using TrocaLivro.Aplicacao.HttpClients;

namespace WebApp.ViewComponents
{
    public class PontosViewComponent : ViewComponent
    {
        private readonly UsuarioApiClient usuarioApiClient;
        private readonly HttpContext httpContext;

        public PontosViewComponent(UsuarioApiClient usuarioApiClient, IHttpContextAccessor httpContextAccessor)
        {
            this.usuarioApiClient = usuarioApiClient;
            this.httpContext = httpContextAccessor.HttpContext;

            if (httpContext.User.Identity != null && httpContext.User.Identity.IsAuthenticated)
                this.usuarioApiClient.AtualizarToken(
                    httpContext.User.Claims.FirstOrDefault(e => e.Type == "Token")
                );
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            int pontosDoUsuario = await usuarioApiClient.ObterPontos();
            return View(new PontosViewModel(pontosDoUsuario));
        }
    }
}