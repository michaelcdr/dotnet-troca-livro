using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;

namespace WebApp.Filtros
{
    /// <summary>
    /// Limita acesso a usuários autorizados, se nao autorizado volta para o login.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeCustomizadoAttribute : Attribute, IAuthorizationFilter 
    { 
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.HttpContext.User == null || !context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new RedirectToRouteResult(
                    new RouteValueDictionary{
                        { "controller", "Usuario" },
                        { "action", "Login" }
                    }
                );
            }
        }
    }
}