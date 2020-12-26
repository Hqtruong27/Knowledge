using Knowledge.Backend.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Knowledge.Backend.Data.Configurations
{
    public static class FluentApi
    {
        public static void ConfigurationFluentApi(this ModelBuilder builder)
        {
            builder.Entity<IdentityRole>()
                  .Property(x => x.Id).HasMaxLength(50).IsUnicode(false);

            builder.Entity<User>()
                   .Property(x => x.Id).HasMaxLength(50).IsUnicode(false);

            builder.Entity<LabelInKnowedgeBase>()
                   .Property(x => new { x.LabelId, x.KnowledgeBaseId });

            builder.Entity<Vote>()
                   .Property(x => new { x.KnowledgeBaseId, x.UserId });

            builder.Entity<CommandInFunction>()
                   .Property(x => new { x.CommandId, x.FunctionId });

            builder.HasSequence("KnowledgeBaseSequence");
        }
    }
}
