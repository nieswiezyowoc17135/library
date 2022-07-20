using Library.Services.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;


namespace Library.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;
        public EmailService(IConfiguration config) {
            _config = config;
        }

        public void SendEmail()
        {
            //cialo całego emaila
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("nieprzyjemnasytuacja@gmail.com"));
            email.To.Add(MailboxAddress.Parse("nieprzyjemnasytuacja@gmail.com"));
            email.Subject = "Pobranie pliku";
            email.Body = new TextPart(TextFormat.Html) { Text = "Sprawdz folder downloads" };

            using var smtp = new SmtpClient();
            smtp.Connect(_config.GetSection("EmailHost").Value,  587, SecureSocketOptions.StartTls);
            smtp.Authenticate(_config.GetSection("EmailUsername").Value, _config.GetSection("EmailPassword").Value);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
