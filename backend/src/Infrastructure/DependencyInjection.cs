using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TechNova.Core.Interfaces;
using TechNova.Infrastructure.Data;
using TechNova.Infrastructure.Services;
using TechNova.Infrastructure.Settings;

namespace TechNova.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection("Jwt"));

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection") ??
                                   "Server=(localdb)\\mssqllocaldb;Database=TechNova;Trusted_Connection=True;";
            options.UseSqlServer(connectionString);
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<JwtTokenService>();

        return services;
    }
}
