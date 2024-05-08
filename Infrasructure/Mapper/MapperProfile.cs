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
			var types = Assembly
				.GetExecutingAssembly()
				.GetExportedTypes()
				.Where(type => type.GetInterfaces()
					.Any(i => !i.IsGenericType && i.IsTypeDefinition && i == typeof(IViewModel)))
				.ToList();

			foreach (var type in types)
			{
				var instance = Activator.CreateInstance(type);
				var methodInfo = type.GetMethod("Mapping");

				methodInfo.Invoke(instance, new object[] { this });
			}
		}
	}
}
