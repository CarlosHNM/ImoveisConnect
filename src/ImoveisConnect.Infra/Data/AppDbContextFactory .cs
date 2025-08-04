using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ImoveisConnect.Infra.Data
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

            // caminho até a API (onde estão os appsettings)
            var basePath = Path.Combine(
                Directory.GetCurrentDirectory(), "../ImoveisConnect.API");

            var cfg = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: true)
                .AddJsonFile($"appsettings.{env}.json", optional: true)
                .Build();

            var cs = cfg.GetConnectionString("DefaultConnection");
            if (string.IsNullOrEmpty(cs))
                throw new InvalidOperationException("ConnectionString NÃO encontrada.");

            var opt = new DbContextOptionsBuilder<AppDbContext>()
                      .UseSqlServer(cs)
                      .Options;

            return new AppDbContext(opt);
        }
    }
}
