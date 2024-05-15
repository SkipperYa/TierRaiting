using AutoMapper;

namespace Infrastructure.Mapper
{
	public interface ICustomMapping<TSource, TDestination>
		where TDestination : class
		where TSource : class
	{
		void CustomMapping(IMappingExpression<TSource, TDestination> expression);
	}
}
