using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Exceptions;
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
            try
            {
                await transportWeb.DeliverAsync(myMessage);
            }
            //http://stackoverflow.com/questions/28878924/bad-request-check-errors-for-a-list-of-errors-returned-by-the-api-at-sendgrid
            catch (InvalidApiRequestException ex)
            {
                var errorDetails = new StringBuilder();

                errorDetails.Append("ResponseStatusCode: " + ex.ResponseStatusCode + ".   ");
                for (int i = 0; i < ex.Errors.Count(); i++)
                {
                    errorDetails.Append($" -- Error #{i} : {ex.Errors[i]}");
                }

                throw new ApplicationException(errorDetails.ToString(), ex);
            }
        }
    }
}