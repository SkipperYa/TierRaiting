using Domain.Entities;
using Domain.Enum;
using Domain.Exceptions;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Queries
{
	public class GetItemOptionsQueryHandler : BaseHandler<GetItemOptionsQuery, List<ItemOption>>
	{
		private readonly ISteamService _steamService;
		private readonly IBooksService _booksService;

		public GetItemOptionsQueryHandler(ISteamService steamService, IBooksService booksService)
		{
			_steamService = steamService;
			_booksService = booksService;
		}

		public override async Task<List<ItemOption>> Handle(GetItemOptionsQuery request, CancellationToken cancellationToken)
		{
			switch (request.CategoryType)
			{
				case CategoryType.Games:
				{
					return await _steamService.GetOptions(request.Text, cancellationToken);
				}
				case CategoryType.Books:
				{
					return await _booksService.GetOptions(request.Text, cancellationToken);
				}
				case CategoryType.Films:
				{
					throw new NotImplementedException();
				}
				default:
				{
					throw new LogicException("Invalid CategoryType");
				}
			}
		}
	}
}
