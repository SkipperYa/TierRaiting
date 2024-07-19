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
	public class ImageLinks
	{
		[JsonPropertyName("smallThumbnail")]
		public string SmallThumbnail { get; set; }

		[JsonPropertyName("thumbnail")]
		public string Thumbnail { get; set; }
	}

	public class VolumeInfo
	{
		[JsonPropertyName("title")]
		public string Title { get; set; }

		[JsonPropertyName("authors")]
		public List<string> Authors { get; set; }

		[JsonPropertyName("imageLinks")]
		public ImageLinks ImageLinks { get; set; }
	}

	public class BookItem
	{
		[JsonPropertyName("volumeInfo")]
		public VolumeInfo VolumeInfo { get; set; }
	}

	public class GoogleBook 
	{
		[JsonPropertyName("items")]
		public List<BookItem> Items { get; set; }
	}

	public class BooksService : IBooksService
	{
		public static string ClientName = "BooksClient";

		private readonly IHttpClientFactory _clientFactory;

		private readonly GoogleApiOptions _googleApiOptions;

		public BooksService(IHttpClientFactory clientFactory, IOptions<GoogleApiOptions> options)
		{
			_clientFactory = clientFactory;
			_googleApiOptions = options.Value;
		}

		public async Task<List<ItemOption>> GetOptions(string text, CancellationToken cancellationToken)
		{
			text = HttpUtility.UrlEncode(text);

			var client = _clientFactory.CreateClient(ClientName);

			try
			{
				throw new ArgumentException();

				var result = await client.GetFromJsonAsync<GoogleBook>($"?q={text}&key={_googleApiOptions.Key}&maxResults=5", cancellationToken);

				var options = new List<ItemOption>();

				foreach (var item in result?.Items)
				{
					var info = item.VolumeInfo;

					var itemOption = new ItemOption()
					{
						ImgSrc = info.ImageLinks?.Thumbnail,
						Name = $"{info.Authors?.FirstOrDefault()} - {info.Title}",
					};

					options.Add(itemOption);
				}

				return options;
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}
