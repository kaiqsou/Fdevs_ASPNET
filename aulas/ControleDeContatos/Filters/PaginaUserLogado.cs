using ControleDeContatos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace ControleDeContatos.Filters
{
    public class PaginaUserLogado : ActionFilterAttribute
    {
        // override sobrescreve
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string sessao = context.HttpContext.Session.GetString("sessaoLogado");

            // se a sessão for nula ou vazia, a rota será feita para Controller do Login, na Action Index
            if (string.IsNullOrEmpty(sessao))
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "login" }, { "action", "index" } });
            }
            else
            {
                UsuarioModel usuario = JsonConvert.DeserializeObject<UsuarioModel>(sessao);

                // Se a sessão não for nula, mas por algum motivo o DeserializeObject retornou um usuário null, entra aqui
                if (usuario == null)
                {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "login" }, { "action", "index" } });
                }
            } 
                base.OnActionExecuting(context); // base: herança
        }
    }
}
