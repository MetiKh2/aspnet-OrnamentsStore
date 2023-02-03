
namespace Ornaments.Core.Services.Interfaces
{
	public interface IMessageSender
	{
		public Task SendEmailAsync(string email, string title, string text, bool isHTML = false);
	}
}
