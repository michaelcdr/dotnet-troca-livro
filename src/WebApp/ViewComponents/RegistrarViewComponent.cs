using Microsoft.AspNetCore.Mvc;
using TrocaLivro.Aplicacao.CasosDeUsos;

namespace WebApp.ViewComponents
{
    public class RegistrarViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View(new RegistrarUsuarioModel());
        }
    }
}
