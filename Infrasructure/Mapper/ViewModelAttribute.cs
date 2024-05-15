using AutoMapper;
using System;
using System.Linq;

namespace Infrastructure.Mapper
{
	[AttributeUsage(AttributeTargets.Class, Inherited = false)]
	public class ViewModelAttribute<TSource, TDestination> : Attribute, IMapping
		where TDestination : class
		where TSource : class
	{
		private readonly bool _reverseMap;

		public ViewModelAttribute(bool reverseMap = true)
		{
			_reverseMap = reverseMap;
		}

		public void CreateMaps(Profile profile)
		{
			var expression = profile.CreateMap<TSource, TDestination>();

			if (_reverseMap)
			{
				expression.ReverseMap();
			}

			var destinationType = typeof(TDestination);

			if (destinationType.GetInterfaces()
				.Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ICustomMapping<,>)))
			{
				var instance = Activator.CreateInstance(destinationType);

				var methodInfo = destinationType.GetMethod("CustomMapping");

				methodInfo.Invoke(instance, new object[] { expression });
			}
		}
	}
}
