using System.Net;
using System.Net.Mail;

namespace ControleDeContatos.Helpers
{
    public class Email : IEmail
    {
        private readonly IConfiguration _configuration;

        public Email(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool Enviar(string email, string assunto, string mensagem)
        {
            try
            {
                // atribuindo valores de acordo com o que está no app.settings
                string host = _configuration.GetValue<string>("SMTP:Host");
                string nome = _configuration.GetValue<string>("SMTP:Nome");
                string username = _configuration.GetValue<string>("SMTP:UserName");
                string senha = _configuration.GetValue<string>("SMTP:Senha");
                int porta = _configuration.GetValue<int>("SMTP:Porta");

                MailMessage mail = new MailMessage() 
                { 
                    From = new MailAddress(username, nome) 
                };

                // enviar para o e-mail que está vindo como parâmetro
                mail.To.Add(email);

                // assunto a ser exibido no e-mail
                mail.Subject = assunto;

                // mensagem a ser enviada no e-mail
                mail.Body = mensagem;

                // permite passar códigos html para a mensagem do e-mail
                mail.IsBodyHtml = true;

                // faz com que mande o e-mail o mais rápido possível
                mail.Priority = MailPriority.High;

                using (SmtpClient smtp = new SmtpClient(host, porta))
                {
                    smtp.Credentials = new NetworkCredential(username, senha);
                    smtp.EnableSsl = true;

                    smtp.Send(mail);

                    return true;
                }
            }
            catch (Exception erro) 
            {
                return false;
            }
        }
    }
}
