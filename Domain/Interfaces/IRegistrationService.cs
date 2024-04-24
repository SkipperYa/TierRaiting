using Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
	public interface IRegistrationService
	{
		Task<User> Registration(string email, string userName, string password, CancellationToken cancellationToken);
		Task<bool> ConfirmEmail(string userId, string token);
		Task SendConfirmation(User user);
		Task SendConfirmation(string userId);
	}
}
