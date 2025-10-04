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

        public UsuarioModel BuscarPorLogin(string login)
        {
            return _context.Usuarios.FirstOrDefault(x => x.Login.ToUpper() == login.ToUpper());
        }

        public UsuarioModel BuscarPorEmailELogin(string email, string login)
        {
            return _context.Usuarios.FirstOrDefault(x => x.Email.ToUpper() == email.ToUpper() && x.Login.ToUpper() == login.ToUpper());
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
            usuario.SetSenhaHash(); // Hash da senha

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

        public UsuarioModel AlterarSenha(AlterarSenhaModel alterarSenhaModel)
        {
            // Procura o usuário pelo id fornecido na model AlterarSenha no momento da alteração
            UsuarioModel usuarioDb = ListarPorId(alterarSenhaModel.Id);

            // Verifica se existe o usuário no banco
            if (usuarioDb == null) throw new Exception("Houve um erro na atualização da senha: usuário não encontrado");

            // Utiliza o método de verificação da senha válida para comparar as senhas - a senhaAtual da página de alterar senha e a senha do usuário
            if (!usuarioDb.SenhaValida(alterarSenhaModel.SenhaAtual)) throw new Exception("Senha atual não confere");

            // Não permite alterar a senha atual para a mesma senha
            if (usuarioDb.SenhaValida(alterarSenhaModel.NovaSenha)) throw new Exception("A nova senha deve ser diferente da senha atual");

            // Se não ocorrer nenhum dos erros acima, atualiza a senha
            usuarioDb.SetNovaSenha(alterarSenhaModel.NovaSenha);
            usuarioDb.DataAtualizacao = DateTime.Now; 

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
