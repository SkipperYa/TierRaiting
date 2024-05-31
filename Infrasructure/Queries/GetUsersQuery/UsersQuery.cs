using Domain.Entities;
using Domain.Models;
using Infrastructure.BaseRequest;

namespace Infrastructure.Queries
{
	public class UsersQuery : BaseAuthorizeListRequest<PagedList<UserViewModel>>
	{

	}
}
