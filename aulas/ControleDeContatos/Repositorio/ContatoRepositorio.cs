using ControleDeContatos.Data;
using ControleDeContatos.Models;

namespace ControleDeContatos.Repositorio
{
    public class ContatoRepositorio : IContatoRepositorio
    {
        private readonly BancoContext _context;
        public ContatoRepositorio(BancoContext bancoContext)
        {
            this._context = bancoContext;
        }
            
        public ContatoModel ListarPorId(int id)
        {
            // banco -> tabela -> buscar o primeiro ou unico registro dela onde x.Id é igual a 'id'
            return _context.Contatos.FirstOrDefault(x => x.Id == id);        
        }   

        public List<ContatoModel> BuscarTodos()
        {
            // carrega tudo que está na tabela de contatos
            return _context.Contatos.ToList(); 
        }

        public ContatoModel Adicionar(ContatoModel contato)
        {
            // Gravar no Banco de Dados
            _context.Contatos.Add(contato);
            _context.SaveChanges();

            return contato;
        }

        public ContatoModel Atualizar(ContatoModel contato)
        {
            // Procurando usuário por Id
            ContatoModel contatoDb = ListarPorId(contato.Id);

            // Enviando mensagem de erro, caso retorne Nulo a pesquisa por Id
            if (contatoDb == null) throw new System.Exception("Houve um erro na atualização do contato!");

            // Alterando os dados originais para os dados enviados via POST
            contatoDb.Nome = contato.Nome;
            contatoDb.Email = contato.Email;
            contatoDb.Celular = contato.Celular;

            // Atualizar dados no Banco de Dados
            _context.Contatos.Update(contatoDb);
            _context.SaveChanges();

            return contatoDb;
        }

        public bool Excluir(int id)
        {
            ContatoModel contatoDb = ListarPorId(id);

            if (contatoDb == null) throw new System.Exception("Houve um erro na exclusão do contato!");

            // Excluindo contato no Banco de Dados
            _context.Contatos.Remove(contatoDb);
            _context.SaveChanges();

            return true;
        }
    }
}
