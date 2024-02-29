using Infrastructure.BaseRequest;
using Domain.Entities;
using Domain.Interfaces;
using System;

namespace Infrastructure.Commands
{
	public class DeleteCategoryCommand : BaseAuthorizeRequest<Category>, IWithId
	{
		public Guid Id { get; set; }
	}
}
