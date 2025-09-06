using ControleDeContatos.Data;
using ControleDeContatos.Models;

namespace ControleDeContatos.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly BancoContext _context;
        public UsuarioRepositorio(BancoContext bancoContext)
        {
            this._context = bancoContext;
        }
            
        public UsuarioModel ListarPorId(int id)
        {
            // banco -> tabela -> buscar o primeiro ou unico registro dela onde x.Id é igual a 'id'
            return _context.Usuarios.FirstOrDefault(x => x.Id == id);        
        }   

        public List<UsuarioModel> BuscarTodos()
        {
            // carrega tudo que está na tabela de contatos
            return _context.Usuarios.ToList(); 
        }

        public UsuarioModel Adicionar(UsuarioModel usuario)
        {
            // Gravar no Banco de Dados
            usuario.DataCadastro = DateTime.Now;

            _context.Usuarios.Add(usuario);
            _context.SaveChanges();

            return usuario;
        }

        public UsuarioModel Atualizar(UsuarioModel usuario)
        {
            // Procurando usuário por Id
            UsuarioModel usuarioDb = ListarPorId(usuario.Id);

            // Enviando mensagem de erro, caso retorne Nulo a pesquisa por Id
            if (usuarioDb == null) throw new System.Exception("Houve um erro na atualização do usuário!");

            // Alterando os dados originais para os dados enviados via POST
            usuarioDb.Nome = usuario.Nome;
            usuarioDb.Email = usuario.Email;
            usuarioDb.Login = usuario.Login;
            usuarioDb.Perfil = usuario.Perfil;
            usuarioDb.DataAtualizacao = DateTime.Now;

            // Atualizar dados no Banco de Dados
            _context.Usuarios.Update(usuarioDb);
            _context.SaveChanges();

            return usuarioDb;
        }

        public bool Excluir(int id)
        {
            UsuarioModel usuarioDb = ListarPorId(id);

            if (usuarioDb == null) throw new Exception("Houve um erro na exclusão do usuário!");

            // Excluindo contato no Banco de Dados
            _context.Usuarios.Remove(usuarioDb);
            _context.SaveChanges();

            return true;
        }
    }
}
