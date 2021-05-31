using System.Collections.Generic;

namespace BlogApplication.Domain.Interfaces.Mapping
{
    public interface IModelMapper
    {
        IEnumerable<TDestination> Map<TSource, TDestination>(IEnumerable<TSource> sources);

        IEnumerable<TDestination> Map<TSource, TDestination>(
            IEnumerable<TSource> sources,
            IEnumerable<TDestination> destinations);

        TDestination Map<TSource, TDestination>(TSource source);

        TDestination Map<TSource, TDestination>(TSource source, TDestination destination);
    }
}