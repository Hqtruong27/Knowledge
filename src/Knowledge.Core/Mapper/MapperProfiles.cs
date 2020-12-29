using AutoMapper;
using Knowledge.Services.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

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
