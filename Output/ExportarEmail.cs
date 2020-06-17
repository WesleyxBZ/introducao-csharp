using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using introducao_csharp.Entidades;

namespace introducao_csharp.Output
{
    public class ExportarEmail : IExportar
    {
        public string NomeArquivo { get; set; }

        public ExportarEmail(string nomeArquivo)
        {
            NomeArquivo = nomeArquivo;
        }

        public void Exportar(List<IExportarDados> dados)
        {
            Console.Clear();
            Console.WriteLine("== Exportar para email ==");

            Console.WriteLine("\nInforme seu email:");
            var remetenteEmail = Console.ReadLine();

            Console.WriteLine("\nInforme sua senha:");
            var remetenteSenha = Console.ReadLine();

            Console.WriteLine("\nInforme o email do destinatário:");
            var destinatarioEmail = Console.ReadLine();

            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress("email@email.com", "Sistema Ger. Clientes");
                mail.To.Add(destinatarioEmail);
                mail.Subject = "Backup clientes";
                mail.Body = "Segue em anexo o arquivo de backup!";
                mail.IsBodyHtml = true;
                Attachment data = new Attachment("bd-clientes", MediaTypeNames.Application.Xml);
                mail.Attachments.Add(data);

                using (SmtpClient client = new SmtpClient("smtp.gmail.com", 587))
                {
                    client.Credentials = new NetworkCredential(remetenteEmail, remetenteSenha);
                    client.EnableSsl = true;
                    client.Send(mail);
                }
            }

            Console.WriteLine("Email enviado, pressione uma tecla para voltar");
            Console.ReadKey();
        }
    }
}