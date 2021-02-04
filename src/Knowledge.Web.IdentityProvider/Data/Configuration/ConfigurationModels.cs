using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Knowledge.Web.IdentityProvider.Data.Configuration
{
    public static class ConfigurationModels
    {
        public static void Configuration(this ModelBuilder builder)
        {
            builder.Entity<IdentityRole>()
                  .Property(x => x.Id).HasMaxLength(50).IsUnicode(false);
            builder.Entity<User>()
                   .Property(x => x.Id).HasMaxLength(50).IsUnicode(false);
        }
    }
}
