using Domain.Entities;
using Infrastructure.BaseRequest;

namespace Infrastructure.Commands
{
	public class CreateItemCommand : BaseAuthorizeRequest<Item>
	{
		public string Title { get; set; }
		public string Description { get; set; }
		public string CategoryId { get; set; }
	}
}
