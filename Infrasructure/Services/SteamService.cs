using Domain.Entities;
using Domain.Interfaces;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
	public class SteamService : ISteamService
	{
		public async Task<List<ItemOption>> GetOptions(string text, CancellationToken cancellationToken)
		{
			var steamApps = new List<ItemOption>();

			if (string.IsNullOrEmpty(text))
			{
				return steamApps;
			}

			var web = new HtmlWeb();

			var url = @$"https://store.steampowered.com/search/suggest?term={text}&f=games&cc=EN&realm=1&l=english&v=21880043&excluded_content_descriptors%5B%5D=3&excluded_content_descriptors%5B%5D=4&use_store_query=1&use_search_spellcheck=1&search_creators_and_tags=1";

			var doc = await web.LoadFromWebAsync(url, cancellationToken);

			var nodes = doc.DocumentNode.SelectNodes("a");

			if (nodes is null || !nodes.Any())
			{
				return steamApps;
			}

			foreach (var node in nodes)
			{
				var steamApp = new ItemOption();

				var gameName = node.SelectNodes("div")
					.FirstOrDefault(n => n.Attributes
						.Any(a => a.Name == "class" && a.Value.Contains("match_name"))).InnerText;

				steamApp.Name = gameName;

				var imagesNode = node.SelectNodes("div")
					.FirstOrDefault(n => n.Attributes
						.Any(a => a.Name == "class" && a.Value.Contains("match_img")));

				if (imagesNode != null)
				{
					var gameImg = imagesNode.SelectNodes("img")
						.FirstOrDefault()?.Attributes
							.FirstOrDefault(a => a.Name == "src").Value;

					steamApp.ImgSrc = gameImg;
				}

				steamApps.Add(steamApp);
			}

			return steamApps;
		}
	}
}
