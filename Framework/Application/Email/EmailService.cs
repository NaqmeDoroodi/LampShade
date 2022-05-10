using Framework.Application.Email;
using MimeKit;
using MailKit.Net.Smtp;

namespace Framework.Application.Email
{
    public class EmailService : IEmailService
    {
        public void SendEmail(string title, string messageBody, string destination)
        {
            var message = new MimeMessage();

            var from = new MailboxAddress("DCode", "doroodicode@gmail.com");
            message.From.Add(from);

            var to = new MailboxAddress("User", destination);
            message.To.Add(to);

            message.Subject = title;
            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = $@"<h1> <div style=""background-color = red;""> {messageBody} </div> </h1>",
            };

            message.Body = bodyBuilder.ToMessageBody();

            var client = new SmtpClient();
            client.Connect("smtp.gmail.com", 465, true);
            client.Authenticate("doroodicode@gmail.com", "cpezllyydhnfcdcz");
            client.Send(message);
            client.Disconnect(true);
            client.Dispose();
        }
    }
}