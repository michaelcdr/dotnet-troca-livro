using AutoMapper;
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
using TrocaLivro.Aplicacao.ViewModels;
using TrocaLivro.Dominio.Responses;
using WebApp.Filtros;

namespace WebApp.Controllers
{
    public class UsuarioController : BaseController
    {
        private readonly UsuarioApiClient _usuarioApi;
        private readonly IMapper _mapper;

        public UsuarioController(UsuarioApiClient contaApiClient, IMapper mapper)
        {
            this._usuarioApi = contaApiClient;
            this._mapper = mapper;
        }

        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            
            return View();
        }

        public IActionResult _Logar() => View(new LogarUsuarioModel()); 

        [HttpPost, ModelStateValidador]
        public async Task<JsonResult> Logar(LogarUsuarioModel model)
        {
            if (!ModelState.IsValid) return Json(new { Sucesso = false, Mensagem = "Informe os campos usuário e senha" });

            AppResponse<LogarUsuarioResultado> resultado = await _usuarioApi.Logar(model);

            if (!resultado.Sucesso)
            {
                string mensagem = resultado.Erros != null 
                    ?"<br />" + string.Join("<br/>", resultado.Erros.Select(e => e.Mensagem).ToArray())
                    : resultado.Mensagem;

                return Json(new { Sucesso = false, Mensagem = mensagem });
            }

            await AutenticarRegistrandoClaimns(model.Usuario, resultado);

            return Json(new { sucesso = true, urlDestino = Url.Action("Index", "Home") });            
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
            if (!ModelState.IsValid) return View(model);

            AppResponse<RegistrarUsuarioResultado> resultado = await _usuarioApi.Registrar(model);

            if (!resultado.Sucesso)
            {
                foreach (var item in resultado.Erros)
                    ModelState.AddModelError("", item.Mensagem);

                return View(model);
            }
            
            AppResponse<LogarUsuarioResultado> logarUsuarioResultado = await _usuarioApi.Logar(new LogarUsuarioModel(model.Usuario , model.Senha));

            await AutenticarRegistrandoClaimns(model.Usuario, logarUsuarioResultado);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult AprovarTrocas() => View();

        public async Task<IActionResult> Sair()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [AuthorizeCustomizado]
        public IActionResult MinhaConta()
        {
            return View(new MinhaContaViewModel());
        }

        [AuthorizeCustomizado]
        public async Task<IActionResult> _Editar()
        {
            base.AtualizarToken(this._usuarioApi);
            AppResponse<ObterUsuarioResultado> resultado = await _usuarioApi.Obter(User.Identity.Name);
            var model = _mapper.Map<EditarUsuarioModel>(resultado.Dados);
            return PartialView(model);
        }

        [HttpPost, ModelStateValidador, AuthorizeCustomizado]
        public async Task<JsonResult> _Editar(EditarUsuarioCommand model)
        {
            base.AtualizarToken(this._usuarioApi);
            return Json(await _usuarioApi.Atualizar(model));
        }
    }
}