using AutoMapper;
using BlogApplication.Models.DomainModels.Article;
using BlogApplication.Models.Entities.Article;
using BlogApplication.Models.ViewModels.Article;

namespace BlogApplication.AutoMapper.Profiles.Entities
{
    internal class ArticleCommentEntityProfile : Profile
    {
        public ArticleCommentEntityProfile()
        {
            CreateMap<ArticleCommentEntity, ArticleCommentEntity>();
            MapToArticleCommentViewModel();
            MapToArticleEntity();
            MapToCommentDomainModel();
        }

        private void MapToArticleCommentViewModel()
        {
            CreateMap<ArticleCommentEntity, ArticleCommentViewModel>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.AddedByUser.Id))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.AddedByUser.UserName));
        }

        private void MapToArticleEntity()
        {
            CreateMap<ArticleCommentEntity, ArticleEntity>()
                .ConvertUsing(src => src.Article);
        }

        private void MapToCommentDomainModel()
        {
            CreateMap<ArticleCommentEntity, ArticleCommentDomainModel>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.AddedByUser.Id))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.AddedByUser.UserName));
        }
    }
}