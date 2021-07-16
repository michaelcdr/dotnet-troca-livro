using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TrocaLivro.Dominio.Entidades;
using WebApp.HttpClients;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class ContaController : Controller
    {
        private readonly LivroApClient api;
        private readonly SignInManager<Usuario> _signInManager;
        public ContaController(LivroApClient livroApClient, SignInManager<Usuario> signInManager)
        {
            this.api = livroApClient;
            this._signInManager = signInManager;
        }

        public IActionResult Index() => View(new ContaModel());

        public IActionResult _Logar() => View(new LoginModel()); 

        [HttpPost]
        public async Task<IActionResult> Logar(LoginModel model)
        {
           

            Usuario usuario = await api.ObterUsuario(model.Usuario, model.Senha);
            return View();
        }

        public IActionResult _Registrar() => View(new RegistrarModel());

        public IActionResult AprovarTrocas()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Logar()
        {
            return RedirectToAction("Login");
        }
    }
}
