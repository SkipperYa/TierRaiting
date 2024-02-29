using Infrastructure.BaseRequest;
using Domain.Entities;
using System;
using Domain.Interfaces;

namespace Infrastructure.Commands
{
	public class DeleteItemCommand : BaseAuthorizeRequest<Item>, IWithId
	{
		public Guid Id { get; set; }
	}
}
