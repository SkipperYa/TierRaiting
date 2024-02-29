using Infrastructure.BaseRequest;
using Domain.Entities;
using Domain.Interfaces;
using System;

namespace Infrastructure.Commands
{
	public class UpdateCategoryCommand : BaseAuthorizeRequest<Category>, IWithSrc, IWithId
	{
		public Guid Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string Src { get; set; }
	}
}
