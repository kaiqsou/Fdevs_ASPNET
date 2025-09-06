using ControleDeContatos.Enums;
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
        public PerfilEnum Perfil { get; set; }
        [Required(ErrorMessage = "Por favor, digite uma senha")]
        public string Senha { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataAtualizacao { get; set; }
    }
}
