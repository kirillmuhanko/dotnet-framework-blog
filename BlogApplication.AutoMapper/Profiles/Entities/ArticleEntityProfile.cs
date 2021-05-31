using AutoMapper;
using BlogApplication.Models.DomainModels.Article;
using BlogApplication.Models.Entities.Article;
using BlogApplication.Models.ViewModels.Article;

namespace BlogApplication.AutoMapper.Profiles.Entities
{
    internal class ArticleEntityProfile : Profile
    {
        public ArticleEntityProfile()
        {
            CreateMap<ArticleEntity, ArticleCardTextViewModel>();
            CreateMap<ArticleEntity, ArticleDeleteViewModel>();
            CreateMap<ArticleEntity, ArticleIndexViewModel>();
            CreateMap<ArticleEntity, ArticleViewModel>();
            MapToArticleCardViewModel();
            MapToArticleDomainModel();
            MapToArticleEditViewModel();
            MapToArticleEntity();
        }

        private void MapToArticleCardViewModel()
        {
            CreateMap<ArticleEntity, ArticleCardViewModel>()
                .ForMember(dest => dest.HasImage, opt => opt.MapFrom(src => src.Image.Length != 0));
        }

        private void MapToArticleDomainModel()
        {
            CreateMap<ArticleEntity, ArticleDomainModel>()
                .ForMember(dest => dest.HasImage, opt => opt.MapFrom(src => src.Image.Length != 0));
        }

        private void MapToArticleEditViewModel()
        {
            CreateMap<ArticleEntity, ArticleEditViewModel>()
                .ForMember(dest => dest.Image, opt => opt.Ignore());
        }

        private void MapToArticleEntity()
        {
            CreateMap<ArticleEntity, ArticleEntity>()
                .ForMember(dest => dest.Image, opt => opt.Condition(t => t.Image?.Length != 0));
        }
    }
}