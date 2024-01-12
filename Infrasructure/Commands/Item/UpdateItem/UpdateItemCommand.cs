using Infrastructure.BaseRequest;
using Domain.Entities;

namespace Infrastructure.Commands
{
	public class UpdateItemCommand : BaseAuthorizeRequest<Item>
	{
		public string Title { get; set; }
		public string Description { get; set; }
		public string CategoryId { get; set; }
	}
}
