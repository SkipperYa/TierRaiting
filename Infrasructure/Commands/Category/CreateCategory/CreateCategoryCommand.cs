using Infrastructure.BaseRequest;
using WebApi.Entities;

namespace Infrastructure.Commands
{
	public class CreateCategoryCommand : BaseAuthorizeRequest<Category>
	{
		public string Title { get; set; }
		public string Description { get; set; }
	}
}
