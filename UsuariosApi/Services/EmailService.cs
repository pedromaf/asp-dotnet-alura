using MailKit.Net.Smtp;
using MimeKit;
using Newtonsoft.Json.Linq;
using System.Web;
using UsuariosAPI.Models.Entities;

namespace UsuariosAPI.Services
{
    public class EmailService
    {
        private IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public void SendAccountConfirmationEmail(string recipient, int userId, EmailConfirmationCode code)
        {
            List<string> recipients = new List<string>();
            string emailSubject = "Account confirmation";
            string encodedCode = HttpUtility.UrlEncode(code.Value);
            string emailContent = $"https://localhost:7065/activate?userId={userId}&activationCode={encodedCode}";

            recipients.Add(recipient);

            EmailData emailData = new EmailData(recipients, emailSubject, emailContent);

            MimeMessage email = CreateEmailBody(emailData);

            Send(email);
        }

        private void Send(MimeMessage email)
        {
            using (SmtpClient smtpClient = new SmtpClient())
            {
                try
                {
                    smtpClient.Connect(
                        _config.GetValue<string>("EmailSettings:SmtpServer"),
                        _config.GetValue<int>("EmailSettings:Port"), true);
                    smtpClient.AuthenticationMechanisms.Remove("XOUATH2");
                    smtpClient.Authenticate(
                        _config.GetValue<string>("EmailSettings:From"),
                        _config.GetValue<string>("EmailSettings:Password"));
                    smtpClient.Send(email);
                }
                catch
                {
                    throw;
                }
                finally
                {
                    smtpClient.Disconnect(true);
                    smtpClient.Dispose();
                }
            }
        }

        private MimeMessage CreateEmailBody(EmailData data)
        {
            MimeMessage message = new MimeMessage();

            message.From.Add(new MailboxAddress(null, _config.GetValue<string>("EmailSettings:From")));
            message.To.AddRange(data.Recipients);
            message.Subject = data.Subject;
            message.Body = new TextPart(MimeKit.Text.TextFormat.Text)
            {
                Text = data.Content
            };

            return message;
        }
    }
}
