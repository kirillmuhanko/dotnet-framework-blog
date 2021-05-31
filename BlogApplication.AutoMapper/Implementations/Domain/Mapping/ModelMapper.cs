using System.Collections.Generic;
using AutoMapper;
using BlogApplication.Domain.Interfaces.Mapping;

namespace BlogApplication.AutoMapper.Implementations.Domain.Mapping
{
    public class ModelMapper : IModelMapper
    {
        private readonly IMapper _mapper;

        public ModelMapper(IMapper mapper)
        {
            _mapper = mapper;
        }

        public IEnumerable<TDestination> Map<TSource, TDestination>(
            IEnumerable<TSource> sources,
            IEnumerable<TDestination> destinations)
        {
            var enumerable = _mapper.Map(sources, destinations);
            return enumerable;
        }

        public IEnumerable<TDestination> Map<TSource, TDestination>(IEnumerable<TSource> sources)
        {
            var enumerable = _mapper.Map<IEnumerable<TDestination>>(sources);
            return enumerable;
        }

        public TDestination Map<TSource, TDestination>(TSource source)
        {
            var model = _mapper.Map<TSource, TDestination>(source);
            return model;
        }

        public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            var model = _mapper.Map(source, destination);
            return model;
        }
    }
}