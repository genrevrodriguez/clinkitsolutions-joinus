using CsvHelper;
using CsvHelper.Configuration;
using Fleet.Assets.Requests;
using Fleet.Assets.Services;
using Fleet.Assets.ViewModels;
using Fleet.Files.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Globalization;
using tusdotnet;
using tusdotnet.Interfaces;
using tusdotnet.Models;
using tusdotnet.Models.Configuration;
using tusdotnet.Models.Expiration;
using tusdotnet.Stores;

namespace Microsoft.AspNetCore.Builder
{
    public static class TusConfigurationApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseTus(this IApplicationBuilder appBuilder, IConfiguration configuration)
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
                Events = new Events()
                {
                    OnFileCompleteAsync = async eventContext =>
                    {
                        var assetService = eventContext.HttpContext.RequestServices.GetService<IAssetService>();

                        string fileId = null;

                        try
                        {
                            var file = await eventContext.GetFileAsync();

                            fileId = file.Id;

                            var csvConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture)
                            {
                                HeaderValidated = null,
                                MissingFieldFound = null
                            };

                            using (var fileStream = await file.GetContentAsync(default(CancellationToken)))
                            using (var reader = new StreamReader(fileStream))
                            using (var csv = new CsvReader(reader, csvConfiguration))
                            {
                                var assetUpdates = (csv.GetRecords<AssetUpdateViewModel>() ?? Enumerable.Empty<AssetUpdateViewModel>()).ToList();
                                var validAssetUpdates = assetUpdates.Where(u => (u.AssetId != null || (!string.IsNullOrWhiteSpace(u.AssetName) && !string.IsNullOrWhiteSpace(u.AssetCategory)))
                                    && (u.LocationLatitude.HasValue && u.LocationLongitude.HasValue && u.LocationTimestamp.HasValue));
                                if (!validAssetUpdates.Any())
                                {
                                    throw new InvalidOperationException("Invalid csv data");
                                }
                                else
                                {
                                    foreach (var validAssetUpdate in validAssetUpdates)
                                    {
                                        validAssetUpdate.FileId = fileId;
                                    }

                                    var request = new UpdateAssetLogsRequest()
                                    {
                                        Updates = validAssetUpdates
                                    };

                                    await assetService.UpdateAssetLogsAsync(request);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            if (!string.IsNullOrWhiteSpace(fileId))
                            {
                                try
                                {
                                    var terminationStore = (ITusTerminationStore)eventContext.Store;
                                    await terminationStore.DeleteFileAsync(fileId, default(CancellationToken));
                                }
                                catch { } // silent catch
                            }

                            eventContext.HttpContext.Response.StatusCode = 500;
                        }
                    }
                }
            });
        }
    }
}