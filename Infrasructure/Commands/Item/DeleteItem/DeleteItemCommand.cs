using Infrastructure.BaseRequest;
using Domain.Entities;
using System;
using Domain.Interfaces;
using Infrastructure.Mapper;

namespace Infrastructure.Commands
{
	public class DeleteItemCommand : BaseAuthorizeRequest<Item>, IWithId, IViewModel
	{
		public Guid Id { get; set; }

		public void Mapping(AutoMapper.Profile profile)
		{
			profile.CreateMap<Item, DeleteItemCommand>().ReverseMap();
		}
	}
}
