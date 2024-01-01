using Domain.Entities;
using Domain.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
	public interface ILoginService
	{
		Task<User> Login(LoginViewModel model, CancellationToken cancellationToken);
		Task Logout(CancellationToken cancellationToken);
		string GetToken(User user);
	}
}
