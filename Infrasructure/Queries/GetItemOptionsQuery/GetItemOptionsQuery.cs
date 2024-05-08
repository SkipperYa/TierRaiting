﻿using Domain.Entities;
using Domain.Enum;
using Infrastructure.BaseRequest;
using System.Collections.Generic;

namespace Infrastructure.Queries
{
	public class GetItemOptionsQuery : BaseAuthorizeRequest<List<ItemOption>>
	{
		public string Text { get; set; }
		public CategoryType CategoryType { get; set; }
	}
}
