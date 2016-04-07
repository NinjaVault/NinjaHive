using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using SendGrid;

namespace NinjaHive.WebApp.Services
{
    public class SendGridEmailService : IIdentityMessageService
    {
        private readonly string fromAddress;
        private readonly NetworkCredential credentials;

        public SendGridEmailService(string mailAccount, string mailPassword, string fromAddress)
        {
            this.fromAddress = fromAddress;
            this.credentials = new NetworkCredential(mailAccount, mailPassword);
        }

        public async Task SendAsync(IdentityMessage message)
        {
            await this.ConfigSendGridasync(message);
        }

        // Use NuGet to install SendGrid (Basic C# client lib) 
        private async Task ConfigSendGridasync(IdentityMessage message)
        {
            var myMessage = new SendGridMessage();
            myMessage.AddTo(message.Destination);
            myMessage.From = new MailAddress(this.fromAddress, "NinjaHive System");
            myMessage.Subject = message.Subject;
            myMessage.Text = message.Body;
            myMessage.Html = message.Body;

            // Create a Web transport for sending email.
            var transportWeb = new Web(this.credentials);

            // Send the email.
            await transportWeb.DeliverAsync(myMessage);
        }
    }
}