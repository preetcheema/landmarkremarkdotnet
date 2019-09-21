using System.Diagnostics;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace LandmarkRemark.Persistence.Infrastructure
{
    public class LandmarkRemarkContextFactory : IDesignTimeDbContextFactory<LandmarkRemarkContext>
    {
        private const string ConnectionStringName = "LandmarkRemarkDatabase";
        public LandmarkRemarkContext CreateDbContext(string[] args)
        {
            var basePath = Directory.GetCurrentDirectory() + string.Format("{0}..{0}LandmarkRemark.Api", Path.DirectorySeparatorChar);

            var configuration = new ConfigurationBuilder()
                  .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.Local.json", optional: true)               
                .Build();

            var connectionString = configuration.GetConnectionString(ConnectionStringName);

            var optionsBuilder = new DbContextOptionsBuilder<LandmarkRemarkContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new LandmarkRemarkContext(optionsBuilder.Options);
        }
    }

   
}