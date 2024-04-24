using System.Threading.Tasks;

namespace Domain.Interfaces
{
	public interface IEmailService
	{
		Task Send(string to, string subject, string message);
	}
}
