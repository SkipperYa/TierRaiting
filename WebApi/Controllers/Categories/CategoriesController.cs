﻿using Infrastructure.Queries.GetCategoriesQuery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApi.Controllers.Categories
{
	[Authorize]
	public class CategoriesController : BaseApplicationController
	{
		public CategoriesController(IMediator mediator) : base(mediator)
		{
		}

		[HttpGet]
		public async Task<IActionResult> GetCategories(int page)
		{
			var result = await _mediator.Send(new CategoriesQuery()
			{
				UserId = UserId,
				Page = page
			});

			return Ok(result);
		}
	}
}
