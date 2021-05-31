using AutoMapper;
using BlogApplication.Models.Entities.User;
using BlogApplication.Models.ViewModels.Admin;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BlogApplication.AutoMapper.Profiles.Entities
{
    internal class RoleEntityProfile : Profile
    {
        public RoleEntityProfile()
        {
            CreateMap<RoleEntity, AdminEditRoleViewModel>();
            CreateMap<RoleEntity, IdentityUserRole>().ReverseMap();
        }
    }
}