using Infrastructure.BaseRequest;
using System.ComponentModel.DataAnnotations;
using WebApi.Entities;

namespace Infrastructure.Commands
{
	public class CreateCategoryCommand : BaseAuthorizeRequest<Category>
	{
		public CreateCategoryCommand([Required] string userId) : base(userId)
		{
		}

		public string Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
	}
}
