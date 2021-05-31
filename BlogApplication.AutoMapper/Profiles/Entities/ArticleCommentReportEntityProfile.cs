using AutoMapper;
using BlogApplication.Models.Entities.Article;
using BlogApplication.Models.ViewModels.Article;

namespace BlogApplication.AutoMapper.Profiles.Entities
{
    internal class ArticleCommentReportEntityProfile : Profile
    {
        public ArticleCommentReportEntityProfile()
        {
            CreateMap<ArticleCommentReportEntity, ArticleCommentReportEntity>();
            MapToArticleCommentReportViewModel();
        }

        private void MapToArticleCommentReportViewModel()
        {
            CreateMap<ArticleCommentReportEntity, ArticleCommentReportViewModel>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.AddedByUser.Id))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.AddedByUser.UserName));
        }
    }
}