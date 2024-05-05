using AutoMapper;
using Domain.Entities;
using Domain.Models;
using Infrastructure.Database;
using Infrastructure.Queries;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Queries
{
	public class GetUserQueryHandler : BaseGetQueryHandler<GetUserQuery, User, ProfileViewModel>
	{
		public GetUserQueryHandler(ApplicationContext applicationContext, IMapper mapper) : base(applicationContext, mapper)
		{
		}

		public override Task<IQueryable<User>> Filter(IQueryable<User> query, GetUserQuery request, CancellationToken cancellationToken)
		{
			query = query.Where(q => q.Id == request.UserId);

			return base.Filter(query, request, cancellationToken);
		}
	}
}
