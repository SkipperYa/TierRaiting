using Infrastructure.BaseRequest;
using Domain.Entities;
using Domain.Interfaces;
using System;
using Infrastructure.Mapper;

namespace Infrastructure.Commands
{
	[ViewModel<Category, DeleteCategoryCommand>]
	public class DeleteCategoryCommand : BaseAuthorizeRequest<Category>, IWithId
	{
		public Guid Id { get; set; }
	}
}
