using ControleDeContatos.Filters;
using ControleDeContatos.Models;
using ControleDeContatos.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeContatos.Controllers
{
    [PaginaUserAdmin] // Filter
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

        public IActionResult Editar(int id)
        {
            UsuarioModel usuario = _usuarioRepositorio.ListarPorId(id);
            return View(usuario);
        }

        public IActionResult ExcluirConfirmacao(int id)
        {
            UsuarioModel usuario = _usuarioRepositorio.ListarPorId(id);

            return View(usuario);
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

        [HttpPost]
        public IActionResult Alterar(UsuarioSemSenhaModel usuarioSemSenhaModel)
        {
            try
            {
                UsuarioModel usuario = null;

                if (ModelState.IsValid)
                {
                    usuario = new UsuarioModel()
                    {
                        Id = usuarioSemSenhaModel.Id,
                        Nome = usuarioSemSenhaModel.Nome,
                        Login = usuarioSemSenhaModel.Login,
                        Email = usuarioSemSenhaModel.Email,
                        Perfil = usuarioSemSenhaModel.Perfil
                    };

                    _usuarioRepositorio.Atualizar(usuario);

                    TempData["MensagemSucesso"] = "Usuário atualizado com sucesso!";

                    return RedirectToAction("Index");
                }

                // Forçando retornar para a view 'Editar'
                return View("Editar", usuario);
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Erro ao atualizar o usuário! Detalhe do erro: {erro.Message}";

                return RedirectToAction("Index");
            }
        }

        public IActionResult Excluir(int id)
        {
            try
            {
                bool apagado = _usuarioRepositorio.Excluir(id);

                if (apagado)
                {
                    TempData["MensagemSucesso"] = "Usuário excluído com sucesso!";
                }
                else
                {
                    TempData["MensagemErro"] = "Erro na exclusão de usuário!";
                }

                return RedirectToAction("Index");
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Erro na exclusão de usuário! Detalhes do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}
