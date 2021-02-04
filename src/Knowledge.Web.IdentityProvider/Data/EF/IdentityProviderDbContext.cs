using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Knowledge.Web.IdentityProvider.Data.EF
{
    public class IdentityProviderDbContext : IdentityDbContext<User>
    {
        public IdentityProviderDbContext(DbContextOptions<IdentityProviderDbContext> options) : base(options)
        {
        }
    }
}
