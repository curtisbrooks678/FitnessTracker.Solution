using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace FitnessTracker.Models
{
  public class FitnessTrackerContextFactory : IDesignTimeDbContextFactory<FitnessTrackerContext>
  {
    FitnessTrackerContext IDesignTimeDbContextFactory<FitnessTrackerContext>.CreateDbContext(string[] args)
    {
      IConfigurationRoot configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json")
        .Build();
      var builder = new DbContextOptionsBuilder<FitnessTrackerContext>();

      builder.UseMySql(configuration["ConnectionStrings:DefaultConnection"], ServerVersion.AutoDetect(configuration["ConnectionStrings:DefaultConnection"]));
      return new FitnessTrackerContext(builder.Options);
    }
  }
}