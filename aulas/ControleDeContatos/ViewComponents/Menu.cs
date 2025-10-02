using ControleDeContatos.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ControleDeContatos.ViewComponents
{
    public class Menu : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            string sessao = HttpContext.Session.GetString("sessaoLogado");

            if (string.IsNullOrEmpty(sessao)) return null;

            UsuarioModel usuario = JsonConvert.DeserializeObject<UsuarioModel>(sessao);

            return View(usuario);
        }
    }
}
