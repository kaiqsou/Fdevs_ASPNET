using ControleDeContatos.Models;

namespace ControleDeContatos.Helpers
{
    public interface ISessao
    {
        void CriarSessao(UsuarioModel usuario);
        void RemoverSessao();
        UsuarioModel BuscarSessao();
    }
}
