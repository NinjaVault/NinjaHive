using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace NinjaHive.WebApp.Services
{
    public class FakeEmailService : IIdentityMessageService
    {
        private const string path = @"C:\temp";
        private const string file = @"NinjaHiveMailConfirmation.txt";
        private readonly string filePath;

        public FakeEmailService()
        {
            this.filePath = Path.Combine(path, file);
        }

        public async Task SendAsync(IdentityMessage message)
        {
            await this.WriteEmailToDisk(message);
        }

        private Task WriteEmailToDisk(IdentityMessage message)
        {
            Directory.CreateDirectory(path);
            
            File.AppendAllText(this.filePath,
                $"{message.Destination} - {message.Subject}"
                + Environment.NewLine + message.Body + Environment.NewLine +
                "----------------------------------------------------------"
                );

            return Task.FromResult(0);
        }
    }
}
