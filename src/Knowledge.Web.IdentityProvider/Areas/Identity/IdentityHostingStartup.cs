using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity.UI.Services;
using Knowledge.Web.IdentityProvider.Models;
using Knowledge.Web.IdentityProvider.Data.EF;
using Knowledge.Web.IdentityProvider.Data;
using FluentValidation.AspNetCore;
using Knowledge.Web.IdentityProvider.Bussiness.Validation;
using Knowledge.Web.IdentityProvider.Bussiness;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Knowledge.Common.Resources;

[assembly: HostingStartup(typeof(Knowledge.Web.Identity.Areas.Identity.IdentityHostingStartup))]
namespace Knowledge.Web.Identity.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
                //1. DbContext
                services.AddDbContext<IdentityProviderDbContext>(options => {
                    options.UseSqlServer(context.Configuration.GetConnectionString(Constants.IdentityContext));
                });

                //2. Fluent Validation
                services.AddControllers(/*c => c.Filters.Add(new AuthorizeFilter())*/)
                        .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<RoleRequesetValidator>());

                //3. Identity Server
                services.AddIdentity<User, IdentityRole>()
                        .AddEntityFrameworkStores<IdentityProviderDbContext>()
                        .AddDefaultTokenProviders();

                //services.AddIdentityServer(options =>
                //{
                //    options.Events.RaiseErrorEvents = true;
                //    options.Events.RaiseInformationEvents = true;
                //    options.Events.RaiseFailureEvents = true;
                //    options.Events.RaiseSuccessEvents = true;
                //}).AddInMemoryIdentityResources(IdsConfiguration.Ids)
                //      .AddInMemoryApiResources(IdsConfiguration.Apis)
                //      .AddInMemoryClients(IdsConfiguration.Clients)
                //      .AddAspNetIdentity<User>()
                //      .AddDeveloperSigningCredential();

                services.AddIdentityServer()
                        .AddInMemoryIdentityResources(context.Configuration.GetSection("IdentityServer:IdentityResources"))
                        .AddInMemoryApiResources(context.Configuration.GetSection("IdentityServer:ApiResources"))
                        .AddInMemoryClients(context.Configuration.GetSection("IdentityServer:Clients"))
                        .AddDeveloperSigningCredential()
                        .AddAspNetIdentity<User>();                             

                //4. Config Identity options
                services.Configure<IdentityOptions>(options =>
                {
                    //Default Lockout settings.
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                    options.Lockout.MaxFailedAccessAttempts = 5;
                    options.Lockout.AllowedForNewUsers = true;
                    options.SignIn.RequireConfirmedPhoneNumber = false;
                    options.SignIn.RequireConfirmedAccount = false;
                    options.SignIn.RequireConfirmedEmail = false;
                    options.Password.RequiredLength = 6;
                    options.Password.RequireDigit = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.User.RequireUniqueEmail = true;
                    // User Settings.
                    options.User.AllowedUserNameCharacters = Constants.UnsignedChars;
                    options.User.RequireUniqueEmail = false;
                });


                //5. Service
                services.AddHttpContextAccessor();
                services.AddTransient(typeof(IUserManager), typeof(UserManager));
                services.AddTransient<IRoleManager, RoleManager>();
                services.AddTransient<IEmailSender, EmailSender>();
            });
        }
    }
}