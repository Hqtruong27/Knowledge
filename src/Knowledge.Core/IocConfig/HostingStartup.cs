using AutoMapper;
using Knowledge.Common.Resources;
using Knowledge.Core.IocConfig;
using Knowledge.Data.EF;
using Knowledge.Data.Repositories;
using Knowledge.Data.UOW;
using Knowledge.Infrastructure.Mapper;
using Knowledge.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;

[assembly: HostingStartup(typeof(HostingStartup))]
namespace Knowledge.Core.IocConfig
{
    public static class HostingStartup
    {
        public static void RegisterDI(this IServiceCollection services, IConfiguration configuration)
        {
            //1. Database Context
            services.AddSingleton(new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseSqlServer(configuration.GetConnectionString(Constants.DbContext)).Options);
            services.AddDbContext<ApplicationDbContext>(ServiceLifetime.Scoped, ServiceLifetime.Singleton);
            services.AddTransient<IDbContext<ApplicationDbContext>, KnowledgeDbContext>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //3. Resgiter DI Services
            services.AddService();
            //4. Resgiter DI Repository
            services.AddRepository();
            //5. Auto Mapper
            services.AddAutoMapper(typeof(MapperProfiles).Assembly);
            //6. Swagger
            services.AddSwagger();
        }

        public static void AddRepository(this IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
        }
        public static void AddService(this IServiceCollection services)
        {
            services.AddTransient(typeof(IUserService), typeof(UserService));
            services.AddHttpClient<IUserService, UserService>();
        }

        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Knowledge.Web.API", Version = "v1" });
                //c.AddSecurityDefinition(Constants.Bearer, new OpenApiSecurityScheme
                //{
                //    Type = SecuritySchemeType.OAuth2,
                //    Flows = new OpenApiOAuthFlows
                //    {
                //        Implicit = new OpenApiOAuthFlow
                //        {
                //            AuthorizationUrl = new Uri("https://localhost:4000/connect/authorize"),
                //            Scopes = new Dictionary<string, string> { {"bookstore_apis", "Knowledge API" } }
                //        },
                //    },
                //});
                //c.AddSecurityRequirement(new OpenApiSecurityRequirement
                //{
                //    {
                //        new OpenApiSecurityScheme
                //        {
                //                Reference  = new OpenApiReference{Type = ReferenceType.SecurityScheme, Id = Constants.Bearer},
                //                Scheme = "oauth2",  Name = Constants.Bearer, In = ParameterLocation.Header,
                //        },
                //        new List<string>(){ "bookstore_apis" }
                //    }
                //});
            });

            services.AddControllers(/*o => o.Filters.Add(new AuthorizeFilter())*/);
            services.AddHttpContextAccessor();
            services.AddAuthentication(o =>
            {
                o.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            }).AddCookie().AddOpenIdConnect(options =>
            {
                options.Authority = "https://localhost:4000";
                options.ClientId = "bookstore_webapp";
                options.ClientSecret = "supersecret";
                options.CallbackPath = "/signin-oidc";
                options.Scope.Add("openid");
                options.Scope.Add("bookstore");
                options.Scope.Add("bookstore_apis");
                options.Scope.Add("bookstore_viewbook");
                //options.Scope.Add("offline_access");

                options.SaveTokens = true;
                options.ResponseType = "code";
                options.ResponseMode = "form_post";

                options.UsePkce = true;
            });
        }
    }
}