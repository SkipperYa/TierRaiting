using AutoMapper;
using System;
using System.Linq;
using System.Reflection;

namespace Infrastructure.Mapper
{
	public class MapperProfile : Profile
	{
		public MapperProfile()
		{
			var items = Assembly
				.GetExecutingAssembly()
				.GetExportedTypes()
				.Where(type => Attribute.IsDefined(type, typeof(ViewModelAttribute<,>)))
				.Select(type => type.GetCustomAttribute(typeof(ViewModelAttribute<,>)) as IMapping)
				.Where(a => a is not null)
				.ToList();

			foreach (var item in items)
			{
				item.CreateMaps(this);
			}
		}
	}
}
