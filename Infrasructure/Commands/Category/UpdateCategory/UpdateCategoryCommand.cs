using Infrastructure.BaseRequest;
using Domain.Entities;
using Domain.Interfaces;

namespace Infrastructure.Commands
{
	public class UpdateCategoryCommand : BaseAuthorizeRequest<Category>, IWithSrc
	{
		public string Title { get; set; }
		public string Description { get; set; }
		public string Src { get; set; }
	}
}
