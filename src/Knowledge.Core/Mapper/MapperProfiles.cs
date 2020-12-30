using AutoMapper;
using Knowledge.Services.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace Knowledge.Core.Mapper
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            CreateMap<RoleViewModel, IdentityRole>();
            CreateMap<IdentityRole, RoleViewModel>();
        }
    }
}
