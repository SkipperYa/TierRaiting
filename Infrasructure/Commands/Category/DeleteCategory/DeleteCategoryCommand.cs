using Infrastructure.BaseRequest;
using Domain.Entities;
using Domain.Interfaces;
using System;
using Infrastructure.Mapper;

namespace Infrastructure.Commands
{
	public class DeleteCategoryCommand : BaseAuthorizeRequest<Category>, IWithId, IViewModel
	{
		public Guid Id { get; set; }

		public void Mapping(AutoMapper.Profile profile)
		{
			profile.CreateMap<Category, DeleteCategoryCommand>().ReverseMap();
		}
	}
}
