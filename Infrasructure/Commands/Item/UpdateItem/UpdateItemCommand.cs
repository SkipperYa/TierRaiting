﻿using Infrastructure.BaseRequest;
using Domain.Entities;
using Domain.Interfaces;
using System;
using Domain.Enum;
using Infrastructure.Mapper;

namespace Infrastructure.Commands
{
	[ViewModel<Item, UpdateItemCommand>]
	public class UpdateItemCommand : BaseAuthorizeRequest<Item>, IWithId, IWithSrc
	{
		public Guid Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string CategoryId { get; set; }
		public Tier Tier { get; set; }
		public string Src { get; set; }
	}
}
