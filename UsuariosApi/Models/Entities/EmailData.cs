using MimeKit;
using System.Net.Mail;

namespace UsuariosAPI.Models.Entities
{
    public class EmailData
    {
        public List<MailboxAddress> Recipients { get; }
        public string Subject { get; }
        public string Content { get; set; }

        public EmailData(List<string> recipients, string subject, string content)
        {
            Recipients = new List<MailboxAddress>();
            Subject = subject;
            Content = content;

            Recipients.AddRange(recipients.Select(recipient => new MailboxAddress(null, recipient)));
        }
    }
}
