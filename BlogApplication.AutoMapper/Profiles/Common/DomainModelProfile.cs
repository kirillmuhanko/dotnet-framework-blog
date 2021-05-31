using AutoMapper;
using BlogApplication.Models.DomainModels.Article;
using BlogApplication.Models.DomainModels.Common;
using BlogApplication.Models.DomainModels.User;
using BlogApplication.Models.ViewModels.Admin;
using BlogApplication.Models.ViewModels.Article;
using BlogApplication.Models.ViewModels.Common;
using Microsoft.AspNet.Identity;

namespace BlogApplication.AutoMapper.Profiles.Common
{
    internal class DomainModelProfile : Profile
    {
        public DomainModelProfile()
        {
            CreateMap<ArticleDomainModel, ArticleIndexViewModel>();
            CreateMap<IdentityResult, ResultStatusDomainModel>();
            CreateMap<PaginationDomainModel, PaginationViewModel>();
            CreateMap<UserRoleDomainModel, AdminEditRoleViewModel>().ReverseMap();
        }
    }
}