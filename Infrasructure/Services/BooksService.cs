using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
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
	public class OpenBookDocs
	{
		[JsonPropertyName("key")]
		public string Key { get; set; }

		[JsonPropertyName("author_name")]
		public List<string> AuthorName { get; set; }

		[JsonPropertyName("title")]
		public string Title { get; set; }
	}

	public class OpenBook 
	{
		[JsonPropertyName("docs")]
		public List<OpenBookDocs> Docs { get; set; }
	}

	public class BooksService : IBooksService
	{
		public static string ClientName = "BooksClient";

		private readonly IHttpClientFactory _clientFactory;

		public BooksService(IHttpClientFactory clientFactory)
		{
			_clientFactory = clientFactory;
		}

		public async Task<List<ItemOption>> GetOptions(string text, CancellationToken cancellationToken)
		{
			text = HttpUtility.UrlEncode(text);

			// text = "the+lord+of+the+rings";

			var client = _clientFactory.CreateClient(ClientName);

			try
			{
				var result = await client.GetFromJsonAsync<OpenBook>($"?q={text}&fields=key,title,author_name&limit=5", cancellationToken);

				var options = new List<ItemOption>();

				foreach (var item in result?.Docs)
				{
					var itemOption = new ItemOption()
					{
						Name = $"{item.AuthorName.FirstOrDefault()} - {item.Title}",
					};

					options.Add(itemOption);
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
