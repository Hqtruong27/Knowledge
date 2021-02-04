using System;
using AutoMapper;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Identity;

namespace Knowledge.Infrastructure.Mapper
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            //Ex: Read from IdentityRole => RoleResponse
            //CreateMap<IdentityRole, RoleResponse>();
            ////Ex: Map from RoleRequest => IdentityRole
            //CreateMap<RoleRequest, IdentityRole>()
            //    .ForMember(i => i.NormalizedName, r => r.MapFrom(x => x.Name.ToUpper()));
            //CreateMap<UserRequest, User>().ForMember(i => i.NormalizedEmail, x => x.MapFrom(u => u.Email.ToUpper()))
            //                              .ForMember(i => i.NormalizedUserName, x => x.MapFrom(u => u.UserName.ToUpper()));
            //CreateMap<User, UserResponse>();
            //CreateMap<UserResponse, User>();
            //CreateMap<UserRequest, UserResponse>();

        }
    }

    public static class Extensions
    {
        public static IMappingExpression<TSource, TDestination> Ignore<TSource, TDestination>(this IMappingExpression<TSource, TDestination> map, Expression<Func<TDestination, object>> selector)
        {
            map.ForMember(selector, config => config.Ignore());
            return map;
        }
    }
}
