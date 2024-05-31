using AutoMapper;
using Domain.Models;
using Infrastructure.Database;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;

namespace Infrastructure.Queries
{
	public class UsersQueryHandler : BaseListQueryHandler<UsersQuery, User, UserViewModel>
	{
		public UsersQueryHandler(ApplicationContext context, IMapper mapper) : base(context, mapper)
		{
		}

		public override Task<IQueryable<User>> Filters(IQueryable<User> query, UsersQuery request, CancellationToken cancellationToken)
		{
			if (!string.IsNullOrEmpty(request.Text))
			{
				query = query.Where(q => q.Email.Contains(request.Text) || q.UserName.Contains(request.Text));
			}

			return base.Filters(query, request, cancellationToken);
		}
	}
}
