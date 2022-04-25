using Fleet.Assets;
using Fleet.Assets.Models;
using Fleet.Assets.Repositories;
using Fleet.Assets.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class AssetServiceCollectionExtensions
    {
        public static IServiceCollection AddAssetService(this IServiceCollection services, IConfiguration configuration, Assembly containingAssembly)
        {
            services.Configure<AssetDbContextOptions>(configuration);
            services.AddDbContext<AssetDbContext>((serviceProvider, options) =>
            {
                var config = serviceProvider.GetRequiredService<IOptions<AssetDbContextOptions>>();
                options.UseSqlite(config.Value.ConnectionString, sqliteOptions =>
                {
                    sqliteOptions.MigrationsAssembly(containingAssembly.FullName);
                });
            });

            services.AddScoped<IAssetRepository, EfCoreAssetRepository>();
            services.AddScoped<IAssetCategoryRepository, EfCoreAssetCategoryRepository>();
            services.AddScoped<IAssetLogItemRepository, EfCoreAssetLogItemRepository>();
            services.AddScoped<IFleetRepository, EfCoreFleetRepository>();

            services.AddScoped<IAssetService, DefaultAssetService>();
            services.AddScoped<IAssetCategoryService, DefaultAssetCategoryService>();
            services.AddScoped<IFleetService, DefaultFleetService>();

            return services;
        }
    }
}
