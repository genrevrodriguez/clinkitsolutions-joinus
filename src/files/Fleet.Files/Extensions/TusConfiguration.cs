using Fleet.Files.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using tusdotnet;
using tusdotnet.Models;
using tusdotnet.Models.Configuration;
using tusdotnet.Models.Expiration;
using tusdotnet.Stores;

namespace Microsoft.AspNetCore.Builder
{
    public static class TusConfiguration
    {
        public static IApplicationBuilder UseTus(this IApplicationBuilder appBuilder, IConfiguration configuration, Events events)
        {
            var storePath = TusHelper.GetStorePath(configuration);
            if (!Directory.Exists(storePath)) Directory.CreateDirectory(storePath);

            var expirationDays = configuration.GetValue<double?>("Tus:ExpirationDays");
            var expiration = expirationDays != null ? new AbsoluteExpiration(TimeSpan.FromDays(expirationDays.Value)) : null;

            return appBuilder.UseTus(context => new DefaultTusConfiguration()
            {
                Store = TusHelper.GetStore(configuration),
                UrlPath = configuration.GetValue<string>("Tus:UrlPath"),
                MaxAllowedUploadSizeInBytesLong = configuration.GetValue<long?>("Tus:MaxAllowedUploadSizeInBytesLong"),
                Expiration = expiration,
                Events = events
            });
        }
    }
}