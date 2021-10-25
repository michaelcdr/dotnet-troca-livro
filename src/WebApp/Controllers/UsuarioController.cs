using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
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
        private readonly UsuarioApiClient usuarioApi;
        
        public UsuarioController(UsuarioApiClient contaApiClient)
        {
            this.usuarioApi = contaApiClient;
        }

        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            
            return View();
        }

        public IActionResult _Logar() => View(new LogarUsuarioModel()); 

        [HttpPost]
        public async Task<JsonResult> Logar(LogarUsuarioModel model)
        {
            if (ModelState.IsValid)
            {
                AppResponse<LogarUsuarioResultado> resultado = await usuarioApi.Logar(model);

                if (!resultado.Sucesso)
                {
                    string mensagem = resultado.Mensagem;

                    if (resultado.Erros != null)
                        mensagem = "<br />" + string.Join("<br/>", resultado.Erros.Select(e => e.Mensagem).ToArray());

                    return Json(new { Sucesso = false, Mensagem = mensagem });
                }
                else
                {
                    await AutenticarRegistrandoClaimns(model.Usuario, resultado);

                    return Json(new { sucesso = true, urlDestino = Url.Action("Index", "Home") });
                }
            }
            return Json(new { Sucesso = false, Mensagem = "Informe os campos usuário e senha" }) ;
        }

        private async Task AutenticarRegistrandoClaimns(string username, AppResponse<LogarUsuarioResultado> logarUsuarioResultado)
        {
            var claims = new List<Claim>
            {
                new Claim("UsuarioId", logarUsuarioResultado.Dados.UsuarioId),
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.NameIdentifier, username),
                new Claim("Token", logarUsuarioResultado.Dados.Token),
                new Claim(ClaimTypes.Role,logarUsuarioResultado.Dados.Role)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            var authProp = new AuthenticationProperties
            {
                IssuedUtc = DateTime.UtcNow,
                ExpiresUtc = DateTimeOffset.UtcNow.AddDays(2), 
                IsPersistent = true
            };
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, authProp);
        }

        public IActionResult Registrar() => View(new RegistrarUsuarioModel());

        [HttpPost]
        public async Task<ActionResult> Registrar(RegistrarUsuarioModel model)
        {
            if (ModelState.IsValid)
            {
                AppResponse<RegistrarUsuarioResultado> resultado = await usuarioApi.Registrar(model);

                if (!resultado.Sucesso)
                {
                    foreach (var item in resultado.Erros)
                        ModelState.AddModelError("", item.Mensagem);
                }
                else
                {
                    AppResponse<LogarUsuarioResultado> logarUsuarioResultado = await usuarioApi.Logar(new LogarUsuarioModel(model.Usuario , model.Senha));

                    await AutenticarRegistrandoClaimns(model.Usuario, logarUsuarioResultado);

                    return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
        }

        public IActionResult AprovarTrocas() => View();

        public async Task<IActionResult> Sair()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}