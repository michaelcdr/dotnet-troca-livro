using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TrocaLivro.Aplicacao.CasosDeUsos;

namespace WebApp.ViewComponents
{
    public class LoginViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(new LoginModel());
        }
    }
}
