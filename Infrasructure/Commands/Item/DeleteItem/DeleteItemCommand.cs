using Infrastructure.BaseRequest;
using Domain.Entities;
using System;
using Domain.Interfaces;
using Infrastructure.Mapper;

namespace Infrastructure.Commands
{
	[ViewModel<Item, DeleteItemCommand>]
	public class DeleteItemCommand : BaseAuthorizeRequest<Item>, IWithId
	{
		public Guid Id { get; set; }
	}
}
