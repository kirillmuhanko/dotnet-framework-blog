using System.Web;
using AutoMapper;
using BlogApplication.AutoMapper.TypeConverters.Boolean;
using BlogApplication.AutoMapper.TypeConverters.FormGroups;
using BlogApplication.AutoMapper.TypeConverters.Strings;
using BlogApplication.AutoMapper.TypeConverters.Web;
using BlogApplication.Models.Components.Controls;
using BlogApplication.Models.Components.FormGroups;

namespace BlogApplication.AutoMapper.Profiles.Common
{
    internal class TypeConverterProfile : Profile
    {
        public TypeConverterProfile()
        {
            CreateMap<CheckBoxFormGroupComponent, bool>().ConvertUsing<CheckBoxFormGroupComponentToBool>();
            CreateMap<HttpPostedFileBase, byte[]>().ConvertUsing<HttpPostedFileBaseToByteArray>();
            CreateMap<ImageFormGroupComponent, byte[]>().ConvertUsing<ImageFormGroupComponentToByteArray>();
            CreateMap<PasswordFormGroupComponent, string>().ConvertUsing<PasswordFormGroupComponentToString>();
            CreateMap<TextAreaFormGroupComponent, string>().ConvertUsing<TextAreaFormGroupComponentToString>();
            CreateMap<TextBoxFormGroupComponent, string>().ConvertUsing<TextBoxFormGroupComponentToString>();
            CreateMap<UserNameFormGroupComponent, string>().ConvertUsing<UserNameFormGroupComponentToString>();
            CreateMap<bool, CheckBoxFormGroupComponent>().ConvertUsing<BoolToCheckBoxFormGroupComponent>();
            CreateMap<string, LabelComponent>().ConvertUsing<StringToLabelComponent>();
            CreateMap<string, PasswordFormGroupComponent>().ConvertUsing<StringToPasswordFormGroupComponent>();
            CreateMap<string, TextAreaFormGroupComponent>().ConvertUsing<StringToTextAreaFormGroupComponent>();
            CreateMap<string, TextBoxFormGroupComponent>().ConvertUsing<StringToTextBoxFormGroupComponent>();
            CreateMap<string, UserNameFormGroupComponent>().ConvertUsing<StringToUserNameFormGroupComponent>();
        }
    }
}