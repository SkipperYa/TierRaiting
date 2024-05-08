using Domain.Entities;
using Domain.Enum;
using Domain.Interfaces;
using Infrastructure.Commands;
using Infrastructure.Mapper;
using System.Collections.Generic;

namespace Domain.Models
{
	public class CategoryViewModel : IWithSrc, IViewModel
	{
		public string Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public CategoryType CategoryType { get; set; }
		public int ItemsCount { get; set; }
		public string Src { get; set; }
		public List<ItemViewModel> Items { get; set; }

		public void Mapping(AutoMapper.Profile profile)
		{
			profile.CreateMap<Category, CategoryViewModel>().ReverseMap();
		}
	}

	public class CategoryListViewModel : IWithSrc, IViewModel
	{
		public string Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public int ItemsCount { get; set; }
		public string Src { get; set; }
		public CategoryType CategoryType { get; set; }

		public void Mapping(AutoMapper.Profile profile)
		{
			profile.CreateMap<Category, CategoryListViewModel>()
				.ForMember(c => c.ItemsCount, opt => opt.MapFrom(src => src.Items.Count))
				.ReverseMap();
		}
	}
}
