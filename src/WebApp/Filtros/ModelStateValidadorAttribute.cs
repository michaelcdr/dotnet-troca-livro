using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using TrocaLivro.Dominio;
using TrocaLivro.Dominio.Responses;

namespace WebApp.Filtros
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ModelStateValidadorAttribute : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        { 

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var erros = new List<Notificacao>();

                foreach (var item in context.ModelState)
                {
                    if (item.Value.Errors.Count > 0)
                        foreach (var error in item.Value.Errors)
                            erros.Add(new Notificacao(error.ErrorMessage, item.Key));
                }
                
                context.Result = new JsonResult(new AppResponse<dynamic>() { 
                    Sucesso = false, 
                    Mensagem = "Ops, algo de errado.", 
                    Erros = erros
                });
            }
        }
    }
}
