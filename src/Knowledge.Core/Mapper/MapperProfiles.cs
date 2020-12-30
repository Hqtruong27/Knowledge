using AutoMapper;
using Knowledge.Services.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace Knowledge.Core.Mapper
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            CreateMap<RoleViewModel, IdentityRole>()
                .ForMember(i => i.NormalizedName, r => r.MapFrom(x => x.Name.ToUpper()));

            CreateMap<IdentityRole, RoleViewModel>();
        }
    }
}
