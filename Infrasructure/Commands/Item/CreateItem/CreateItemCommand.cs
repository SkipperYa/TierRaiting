using Domain.Entities;
using Domain.Enum;
using Domain.Interfaces;
using Infrastructure.BaseRequest;
using Infrastructure.Mapper;

namespace Infrastructure.Commands
{
	[ViewModel<Item, CreateItemCommand>]
	public class CreateItemCommand : BaseAuthorizeRequest<Item>, IWithSrc
	{
		public string Title { get; set; }
		public string Description { get; set; }
		public string CategoryId { get; set; }
		public Tier Tier { get; set; }
		public string Src { get; set; }
	}
}
