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

            //Permission
            builder.Entity<Permission>()
                       .HasKey(x => new { x.RoleId, x.FunctionId, x.CommandId });
            //Label In Knowledge Base
            builder.Entity<LabelInKnowledgeBase>()
                   .HasKey(x => new { x.LabelId, x.KnowledgeBaseId });
            //Vote
            builder.Entity<Vote>()
                   .HasKey(x => new { x.KnowledgeBaseId, x.UserId });
            //Command In Function
            builder.Entity<CommandInFunction>()
                   .HasKey(x => new { x.CommandId, x.FunctionId });
            //Sequence
            builder.HasSequence("KnowledgeBaseSequence");
        }
    }
}
