using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TrocaLivro.Aplicacao.CasosDeUsos;
using TrocaLivro.Aplicacao.CasosDeUsos.LogarUsuario;
using TrocaLivro.Aplicacao.HttpClients;
using TrocaLivro.Dominio.Responses;

namespace WebApp.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly UsuarioApiClient contaApi;
        
        public UsuarioController(UsuarioApiClient contaApiClient)
        {
            this.contaApi = contaApiClient;
        }

        public IActionResult Login2() => View();

        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            
            return View();
        }

        

        public IActionResult _Logar() => View(new LogarUsuarioModel()); 

        [HttpPost]
        public async Task<IActionResult> Logar(LogarUsuarioModel model)
        {
            if (ModelState.IsValid)
            {
                AppResponse<LogarUsuarioResultado> resultado = await contaApi.Logar(model);

                if (!resultado.Sucesso)
                {
                    foreach (var item in resultado.Erros)
                        ModelState.AddModelError("", item.Mensagem);
                }
                else
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, model.Usuario),
                        new Claim(ClaimTypes.NameIdentifier, model.Usuario),
                        new Claim("Token", resultado.Dados.Token)
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);


                    var authProp = new AuthenticationProperties
                    {
                        IssuedUtc = DateTime.UtcNow,
                        //configurar expiração do cookie para um valor menor que a expiração do token
                        ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(25),
                        IsPersistent = true
                    };
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, authProp);
                    return RedirectToAction("Index","Home");
                }
            }
            return View("~/Views/Usuario/Login.cshtml",model);
        }

        public IActionResult Registrar() => View(new RegistrarUsuarioModel());

        [HttpPost]
        public async Task<ActionResult> Registrar(RegistrarUsuarioModel model)
        {
            
            if (ModelState.IsValid)
            {
                AppResponse<RegistrarUsuarioResultado> resultado = await contaApi.Registrar(model);

                if (!resultado.Sucesso)
                {
                    foreach (var item in resultado.Erros)
                        ModelState.AddModelError("", item.Mensagem);
                }
                else
                {

                }
            }

            return View(model);
        }

        public IActionResult AprovarTrocas()
        {
            return View();
        }

        public async Task<IActionResult> Sair()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}