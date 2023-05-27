using ContasApp.Messages.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ContasApp.Messages.Services
{
    public static class EmailMessageService
    {
        //atributos
        private static string _conta = "cotiaulajava@outlook.com";
        private static string _senha = "@Admin123456";
        private static string _smtp = "smtp-mail.outlook.com";
        private static int _porta = 587;

        //método para fazer o envio das mensagens
        public static void Send(EmailMessageModel model)
        {
            //Preparando o conteudo do email que será enviado
            var mailMessage = new MailMessage(_conta, model.EmailDestinatario);
            mailMessage.Subject = model.Assunto;
            mailMessage.Body = model.Corpo;

            //Enviando o email
            var smtpClient = new SmtpClient(_smtp, _porta);
            smtpClient.EnableSsl = true; //criptografia SSL
            smtpClient.Credentials = new NetworkCredential(_conta, _senha);
            smtpClient.Send(mailMessage);
        }
    }
}