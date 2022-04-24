using Fleet.Files.Services;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class FileServiceCollectionExtensions
    {
        public static IServiceCollection AddFileService(this IServiceCollection services)
        {
            services.AddScoped<IFileService, DefaultFileService>();

            return services;
        }
    }
}
