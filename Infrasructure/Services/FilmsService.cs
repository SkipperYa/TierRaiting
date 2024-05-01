using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Infrastructure.Utils;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Infrastructure.Services
{
	public class FilmInfo
	{
		public string Title { get; set; }
		public string Poster { get; set; }
	}

	public class OMDbResult
	{
		[JsonPropertyName("Response")]
		public string Response { get; set; }

		[JsonPropertyName("Error")]
		public string Error { get; set; }

		[JsonPropertyName("search")]
		public List<FilmInfo> Search { get; set; }
	}

	public class FilmsService : IFilmsService
	{
		public static string ClientName = "FilmsClient";

		private readonly IHttpClientFactory _clientFactory;

		private readonly OMDbOptions _omdbOptions;

		public FilmsService(IHttpClientFactory clientFactory, IOptions<OMDbOptions> options)
		{
			_clientFactory = clientFactory;
			_omdbOptions = options.Value;
		}

		public async Task<List<ItemOption>> GetOptions(string text, CancellationToken cancellationToken)
		{
			text = HttpUtility.UrlEncode(text);

			var client = _clientFactory.CreateClient(ClientName);

			try
			{
				var result = await client.GetFromJsonAsync<OMDbResult>($"?s={text}&apikey={_omdbOptions.Key}&page=1", cancellationToken);

				var options = new List<ItemOption>();

				if (result.Response == "True" && result.Search is { Count: > 0 })
				{
					foreach (var item in result.Search)
					{
						var itemOption = new ItemOption()
						{
							ImgSrc = item.Poster,
							Name = item.Title,
						};

						options.Add(itemOption);
					}
				}

				return options;
			}
			catch (Exception e)
			{
				throw new LogicException(e.Message);
			}
		}
	}
}
