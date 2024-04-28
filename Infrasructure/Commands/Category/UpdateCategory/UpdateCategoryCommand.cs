using Infrastructure.BaseRequest;
using Domain.Entities;
using Domain.Interfaces;
using System;
using Domain.Enum;

namespace Infrastructure.Commands
{
	public class UpdateCategoryCommand : BaseAuthorizeRequest<Category>, IWithSrc, IWithId
	{
		public Guid Id { get; set; }
		public string Title { get; set; }
		public CategoryType CategoryType { get; set; }
		public string Description { get; set; }
		public string Src { get; set; }
	}
}
