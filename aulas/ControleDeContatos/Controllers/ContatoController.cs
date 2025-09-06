using ControleDeContatos.Models;
using ControleDeContatos.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeContatos.Controllers
{
    public class ContatoController : Controller
    {
        private readonly IContatoRepositorio _contatoRepositorio;
        public ContatoController(IContatoRepositorio contatoRepositorio)
        {
            _contatoRepositorio = contatoRepositorio;
        }

        // GETS
        public IActionResult Index()
        {
            List<ContatoModel> contatos = _contatoRepositorio.BuscarTodos();

            return View(contatos);
        }

        public IActionResult Criar()
        {
            return View();
        }

        public IActionResult Editar(int id) // por parâmetro, receberá o ID do contato para editar  
        {
            ContatoModel contato = _contatoRepositorio.ListarPorId(id);

            return View(contato);
        }

        public IActionResult ExcluirConfirmacao(int id)
        {
            ContatoModel contato = _contatoRepositorio.ListarPorId(id);

            return View(contato);
        }

        public IActionResult Excluir(int id)
        {
            try
            {
                bool apagado = _contatoRepositorio.Excluir(id);

                if (apagado)
                {
                    TempData["MensagemSucesso"] = "Contato excluído com sucesso!";
                }
                else
                {
                    TempData["MensagemErro"] = "Erro na exclusão de contato!";
                }

                return RedirectToAction("Index");
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Erro na exclusão de contato! Detalhes do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        // POSTS
        [HttpPost]
        public IActionResult Criar(ContatoModel contato)
        {
            try
            {
                // Se as informações da Model forem válidas, cria o contato
                if (ModelState.IsValid)
                {
                    _contatoRepositorio.Adicionar(contato);

                    // Mensagem de sucesso Temporária
                    TempData["MensagemSucesso"] = "Contato cadastrado com sucesso!";

                    return RedirectToAction("Index");
                }

                // Se não, estoura os erros mas sem perder o conteúdo digitado
                return View(contato);
            }
            catch (Exception erro)
            {
                // Mensagem de erro Temporária
                TempData["MensagemErro"] = $"Não foi possível cadastrar o contato! Tente novamente. Detalhe do erro: {erro.Message}";

                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Alterar(ContatoModel contato)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _contatoRepositorio.Atualizar(contato);

                    TempData["MensagemSucesso"] = "Contato atualizado com sucesso!";

                    return RedirectToAction("Index");
                }

                // Forçando retornar para a view 'Editar'
                return View("Editar", contato);
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Erro ao atualizar o contato! Detalhe do erro: {erro.Message}";

                return RedirectToAction("Index");
            }
        }
    }
}
