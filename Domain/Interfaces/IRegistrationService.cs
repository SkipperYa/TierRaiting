using Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
	public interface IRegistrationService
	{
		Task<User> Registration(string email, string userName, string password, CancellationToken cancellationToken);
		Task<bool> ConfirmEmail(string userId, string token, string email = null, CancellationToken cancellationToken = default);
		Task SendConfirmation(User user, string email = null);
		Task SendConfirmation(string userId, string email = null);
	}
}
