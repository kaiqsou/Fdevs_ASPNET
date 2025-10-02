using ControleDeContatos.Enums;
using ControleDeContatos.Helpers;
using System.ComponentModel.DataAnnotations;

namespace ControleDeContatos.Models
{
    public class UsuarioModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Por favor, digite um nome")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Por favor, digite um login")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Por favor, digite um e-mail")]
        [EmailAddress(ErrorMessage = "O e-mail não é válido!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Informe o perfil do usuário")]
        public PerfilEnum? Perfil { get; set; }
        [Required(ErrorMessage = "Por favor, digite uma senha")]
        public string Senha { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataAtualizacao { get; set; }
        public bool SenhaValida(string senha) { return Senha == senha.GerarHash(); }
        public void SetSenhaHash() { Senha = Senha.GerarHash(); }
    }
}
