using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class CategoriaController :  BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
