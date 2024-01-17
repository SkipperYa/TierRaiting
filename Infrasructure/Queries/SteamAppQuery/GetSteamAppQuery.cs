using Domain.Entities;
using Infrastructure.BaseRequest;
using System.Collections.Generic;

namespace Infrastructure.Queries
{
	public class GetSteamAppQuery : BaseRequest<List<SteamApp>>
	{
		public string Text { get; set; }
	}
}
