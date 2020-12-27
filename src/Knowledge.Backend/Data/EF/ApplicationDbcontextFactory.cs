using Knowledge.Backend.Data.Common.Resources;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Knowledge.Backend.Data.EF
{
    public class ApplicationDbcontextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var x = Environment.GetEnvironmentVariables();
            var env = Environment.GetEnvironmentVariable(Constants.Environment);
            IConfigurationRoot configurationRoot = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json").AddJsonFile($"appsettings.{env}.json")
                            .Build();
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseSqlServer(configurationRoot.GetConnectionString(Constants.KnowledgeDbContext));
            return new ApplicationDbContext(builder.Options);
        }
    }
}
