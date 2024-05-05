using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.BaseRequest;

namespace Infrastructure.Commands.Profile
{
	public class UpdateUserCommand : BaseAuthorizeRequest<User>, IWithSrc
	{
		public string Email { get; set; }
		public string UserName { get; set; }
		public string Src { get; set; }
	}
}
