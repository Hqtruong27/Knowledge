using Knowledge.Common.Resources;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Diagnostics;
using System.IO;

namespace Knowledge.Data.EF
{
    public interface IDbContext<T> where T : class
    {
        T GetContext();
    }
    public class KnowledgeDbContext : IDbContext<ApplicationDbContext>
    {
        private ApplicationDbContext Context;
        private readonly DbContextOptions<ApplicationDbContext> _options;
        public KnowledgeDbContext(DbContextOptions<ApplicationDbContext> options)
        {
            _options = options;
        }
        public ApplicationDbContext GetContext()
        {
            if (Context == null)
            {
                Context = new ApplicationDbContext(_options);
            }
            return Context;
        }
    }
    public class ApplicationDbcontextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            Debugger.Launch();
            var env = Environment.GetEnvironmentVariable(Constants.Environment);
            IConfigurationRoot configuration = new ConfigurationBuilder()
                                    .SetBasePath(Directory.GetCurrentDirectory())
                                    .AddJsonFile($"appsettings.json")
                                    .AddJsonFile($"appsettings.{env}.json", optional: true)
                                    .Build();
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseSqlServer(configuration.GetConnectionString(Constants.DbContext));
            return new ApplicationDbContext(builder.Options);
        }
    }
}
