using Domain.Entities;
using Domain.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Queries
{
	public class GetSteamAppQueryHandler : BaseHandler<GetSteamAppQuery, List<SteamApp>>
	{
		private readonly ISteamService _steamService;

		public GetSteamAppQueryHandler(ISteamService steamService)
		{
			_steamService = steamService;
		}

		public override async Task<List<SteamApp>> Handle(GetSteamAppQuery request, CancellationToken cancellationToken)
		{
			return await _steamService.GetGames(request.Text, cancellationToken);
		}
	}
}
