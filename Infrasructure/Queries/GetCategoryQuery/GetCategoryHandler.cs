using AutoMapper;
using Domain.Models;
using Infrastructure.Database;
using WebApi.Entities;

namespace Infrastructure.Queries
{
	public class GetCategoryHandler : BaseGetQuery<GetCategoryQuery, Category, CategoryViewModel>
	{
		public GetCategoryHandler(ApplicationContext context, IMapper mapper) : base(context, mapper)
		{
		}
	}
}
