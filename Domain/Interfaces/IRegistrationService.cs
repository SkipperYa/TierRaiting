using Domain.Entities;
using Domain.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
	public interface IRegistrationService
	{
		Task<User> Registration(RegistrationViewModel model, CancellationToken cancellationToken);
	}
}
