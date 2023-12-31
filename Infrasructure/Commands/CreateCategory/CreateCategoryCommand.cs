using MediatR;
using WebApi.Entities;

namespace Infrastructure.Commands
{
	public class CreateCategoryCommand : IRequest<Category>
	{
		public string Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string UserId { get; set; }
	}
}
