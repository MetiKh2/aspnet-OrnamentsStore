

using Ornaments.Core.Services.Interfaces;
using System.Net.Mail;
using System.Net;

namespace Ornaments.Core.Services.Implements
{
	public class MessageSender : IMessageSender
	{
		public Task SendEmailAsync(string email, string title, string text, bool isHTML = false)
		{
			using (var client = new SmtpClient())
			{

				var credentials = new NetworkCredential()
				{
					UserName = "metimamadkh", // without @gmail.com
					Password = ""
				};

				client.Credentials = credentials;
				client.Host = "smtp.gmail.com";
				client.Port = 587;
				client.EnableSsl = true;
				using var emailMessage = new MailMessage()
				{
					To = { new MailAddress(email) },
					From = new MailAddress("metimamadkh@gmail.com"), // with @gmail.com
					Subject = title,
					Body = text,
					IsBodyHtml = isHTML
				};

				client.Send(emailMessage);
			}

			return Task.CompletedTask;
		}
	}
}
