using ControleDeContatos.Models;
using Newtonsoft.Json;

namespace ControleDeContatos.Helpers
{
    public class Sessao : ISessao
    {
        private readonly IHttpContextAccessor _httpContext; // responsável pela sessão
        public Sessao(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }
        public UsuarioModel BuscarSessao()
        {
            string sessaoUsuario = _httpContext.HttpContext.Session.GetString("sessaoLogado");

            if (string.IsNullOrEmpty(sessaoUsuario)) return null;

            return JsonConvert.DeserializeObject<UsuarioModel>(sessaoUsuario); // destransforma, transforma novamente em objeto
        }

        public void CriarSessao(UsuarioModel usuario)
        {
            string valor = JsonConvert.SerializeObject(usuario); // convertendo objeto para conseguir usar o SetString

            _httpContext.HttpContext.Session.SetString("sessaoLogado", valor);
        }

        public void RemoverSessao()
        {
            _httpContext.HttpContext.Session.Remove("sessaoLogado");
        }
    }
}
