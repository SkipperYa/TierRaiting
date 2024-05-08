using Infrastructure.BaseRequest;
using Domain.Entities;
using Domain.Interfaces;
using System;
using Domain.Enum;
using Infrastructure.Mapper;

namespace Infrastructure.Commands
{
	public class UpdateCategoryCommand : BaseAuthorizeRequest<Category>, IWithSrc, IWithId, IViewModel
	{
		public Guid Id { get; set; }
		public string Title { get; set; }
		public CategoryType CategoryType { get; set; }
		public string Description { get; set; }
		public string Src { get; set; }

		public void Mapping(AutoMapper.Profile profile)
		{
			profile.CreateMap<Category, UpdateCategoryCommand>().ReverseMap();
		}
	}
}
