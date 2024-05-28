using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;

namespace MYChamp.Controller
{
    public class SendEmail
    {
        public void Send(string toName, string subject, string body)
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress("saikumar", "mailtrap@demomailtrap.com"));
            email.To.Add(new MailboxAddress(toName, "visionvepicbharath@gmail.com"));
            email.Subject = subject;
            email.Body = new TextPart(MimeKit.Text.TextFormat.Plain)
            {
                Text = body
            };

            using (var smtp = new SmtpClient())
            {
                smtp.Connect("live.smtp.mailtrap.io", 587, SecureSocketOptions.StartTls);
                smtp.Authenticate("api", "79d19d14e66841be34ba7ce88519534a");
                smtp.Send(email);
                smtp.Disconnect(true);
            }
        }
    }
}
