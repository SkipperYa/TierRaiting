﻿using Domain.Entities;
using Domain.Enum;
using Domain.Interfaces;
using Infrastructure.BaseRequest;

namespace Infrastructure.Commands
{
	public class CreateItemCommand : BaseAuthorizeRequest<Item>, IWithSrc
	{
		public string Title { get; set; }
		public string Description { get; set; }
		public string CategoryId { get; set; }
		public Tier Tier { get; set; }
		public string Src { get; set; }
	}
}
