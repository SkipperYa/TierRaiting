using AutoMapper;
using Domain.Entities;
using Domain.Enum;
using Domain.Interfaces;
using Infrastructure.Mapper;
using System.Collections.Generic;

namespace Domain.Models
{
	[ViewModel<Category, CategoryViewModel>]
	public class CategoryViewModel : IWithSrc
	{
		public string Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public CategoryType CategoryType { get; set; }
		public string Src { get; set; }
		public List<ItemViewModel> Items { get; set; }
	}

	[ViewModel<Category, CategoryListViewModel>]
	public class CategoryListViewModel : IWithSrc, ICustomMapping<Category, CategoryListViewModel>
	{
		public string Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public int ItemsCount { get; set; }
		public string Src { get; set; }
		public CategoryType CategoryType { get; set; }

		public void CustomMapping(IMappingExpression<Category, CategoryListViewModel> expression)
		{
			expression.ForMember(q => q.ItemsCount, opt => opt.MapFrom(src => src.Items.Count));
		}
	}
}
