using AutoMapper;
using BlogApplication.Models.Entities.Article;
using BlogApplication.Models.Entities.User;
using BlogApplication.Models.ViewModels.Account;
using BlogApplication.Models.ViewModels.Article;

namespace BlogApplication.AutoMapper.Profiles.Common
{
    internal class ViewModelProfile : Profile
    {
        public ViewModelProfile()
        {
            CreateMap<AccountRegisterViewModel, UserEntity>();
            CreateMap<ArticleAddViewModel, ArticleEntity>();
            CreateMap<ArticleCommentReportViewModel, ArticleCommentReportEntity>();
            CreateMap<ArticleEditViewModel, ArticleEntity>();
        }
    }
}