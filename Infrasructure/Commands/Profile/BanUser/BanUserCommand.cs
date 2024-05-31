using Infrastructure.BaseRequest;
using System;

namespace Infrastructure.Commands.Profile.BanUser
{
	public class BanUserCommand : BaseAuthorizeRequest<bool>
	{
		public Guid Id { get; set; }
	}
}
