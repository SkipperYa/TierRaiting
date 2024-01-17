using Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
	public interface ISteamService
	{
		Task<List<SteamApp>> GetGames(string text, CancellationToken cancellationToken);
	}
}
