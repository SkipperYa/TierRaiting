using Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
	public interface ILoginService
	{
		Task<User> Login(string email, string password, CancellationToken cancellationToken);
		Task Logout(CancellationToken cancellationToken);
		string GetToken(User user);
	}
}
