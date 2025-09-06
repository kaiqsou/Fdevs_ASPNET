using ControleDeContatos.Models;
using ControleDeContatos.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeContatos.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        public UsuarioController(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        // GETS
        public IActionResult Index()
        {
            List<UsuarioModel> usuarios = _usuarioRepositorio.BuscarTodos();

            return View(usuarios);
        }

        public IActionResult Criar()
        {
            return View();
        }

        // POSTS
        [HttpPost]
        public IActionResult Criar(UsuarioModel usuario)
        {
            try
            {
                // Se as informações da Model forem válidas, cria o contato
                if (ModelState.IsValid)
                {
                    _usuarioRepositorio.Adicionar(usuario);

                    // Mensagem de sucesso Temporária
                    TempData["MensagemSucesso"] = "Usuário cadastrado com sucesso!";

                    return RedirectToAction("Index");
                }

                // Se não, estoura os erros mas sem perder o conteúdo digitado
                return View(usuario);
            }
            catch (Exception erro)
            {
                // Mensagem de erro Temporária
                TempData["MensagemErro"] = $"Não foi possível cadastrar o usuário! Tente novamente. Detalhe do erro: {erro.Message}";

                return RedirectToAction("Index");
            }
        }
    }
}
