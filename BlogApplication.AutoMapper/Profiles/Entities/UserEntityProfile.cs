using AutoMapper;
using BlogApplication.Infrastructure.Identity;
using BlogApplication.Models.Entities.User;
using BlogApplication.Models.ViewModels.Admin;
using BlogApplication.Models.ViewModels.User;

namespace BlogApplication.AutoMapper.Profiles.Entities
{
    internal class UserEntityProfile : Profile
    {
        public UserEntityProfile()
        {
            CreateMap<UserEntity, AdminEditUserViewModel>();
            CreateMap<UserEntity, ApplicationUser>().ReverseMap();
            CreateMap<UserEntity, UserViewModel>();
        }
    }
}