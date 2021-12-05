using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class EditoraController :  BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
