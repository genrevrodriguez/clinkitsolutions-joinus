using Fleet.Assets.Extensions;
using Fleet.Assets.Models;
using Fleet.Assets.Repositories;
using Fleet.Assets.Requests;
using Fleet.Assets.Responses;
using Fleet.Assets.ViewModels;
using Fleet.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Transactions;

namespace Fleet.Assets.Services
{
    public class DefaultAssetService : IAssetService
    {
        private readonly IAssetRepository _assetRepository;
        private readonly IAssetCategoryRepository _assetCategoryRepository;
        private readonly IFleetRepository _fleetRepository;
        private readonly IAssetLogItemRepository _assetLogItemRepository;

        public DefaultAssetService(IAssetRepository assetRepository,
            IAssetCategoryRepository assetCategoryRepository,
            IFleetRepository fleetRepository,
            IAssetLogItemRepository assetLogItemRepository)
        {
            _assetRepository = assetRepository;
            _assetCategoryRepository = assetCategoryRepository;
            _fleetRepository = fleetRepository;
            _assetLogItemRepository = assetLogItemRepository;
        }

        public async Task<GetAssetsResponse> GetAssetsAsync(GetAssetsRequest request)
        {
            Expression<Func<Asset, bool>> filter = null;
            if (request.FleetId.HasValue)
            {
                Expression<Func<Asset, bool>> fleetIdFilter = v => v.AssetFleets.Any(af => af.FleetId == request.FleetId.Value);

                filter = filter == null 
                    ? fleetIdFilter 
                    : filter.AndAlso(fleetIdFilter);
            }
            if (request.AssetCategoryId.HasValue)
            {
                Expression<Func<Asset, bool>> assetCategoryIdFilter = v => v.AssetCategoryId == request.AssetCategoryId.Value;

                filter = filter == null
                    ? assetCategoryIdFilter
                    : filter.AndAlso(assetCategoryIdFilter);
            }
            if (!string.IsNullOrWhiteSpace(request.FileId))
            {
                Expression<Func<Asset, bool>> fileIdFilter = v => v.AssetLogItems.Any(al => al.FileId == request.FileId);

                filter = filter == null 
                    ? fileIdFilter 
                    : filter.AndAlso(fileIdFilter);
            }

            var assets = filter == null
                ? await _assetRepository.GetAsync()
                : await _assetRepository.GetAsync(filter);
            var viewModels = assets.Select(a => new AssetViewModel
            {
                Id = a.Id,
                Name = a.Name,
                AssetCategory = a.AssetCategory,
                LastKnownLocation = a.AssetLogItems?.FirstOrDefault(al => al?.FileId == request?.FileId)?.Location ?? a.AssetLogItems?.LastOrDefault()?.Location
            });

            var response = new GetAssetsResponse
            {
                Assets = viewModels
            };

            return response;
        }

        public async Task<UpdateAssetLogsResponse> UpdateAssetLogsAsync(UpdateAssetLogsRequest request)
        {
            var updates = request?.Updates?.Where(u => u != null) ?? Enumerable.Empty<AssetUpdateViewModel>();

            var assetIds = updates.Where(u => u.AssetId > 0).Select(u => u.AssetId.Value).Distinct();
            var assetCategoryNames = updates.Where(u => !string.IsNullOrWhiteSpace(u.AssetCategory)).Select(u => u.AssetCategory).Distinct();
            var fleetNames = updates.Where(u => !string.IsNullOrWhiteSpace(u.Fleet)).Select(u => u.Fleet).Distinct();

            var assets = (await _assetRepository.GetAsync((a) => assetIds.Contains(a.Id))).ToList();
            var assetCategories = (await _assetCategoryRepository.GetAsync((ac) => assetCategoryNames.Contains(ac.Name))).ToList();
            var fleets = (await _fleetRepository.GetAsync((f) => fleetNames.Contains(f.Name))).ToList();

            var addedAssetCategories = assetCategoryNames.Where(x => assetCategories.Any(y => y.Name == x) != true).Select(x => new AssetCategory() { Name = x, IconPath = updates.FirstOrDefault(y => y.AssetCategory == x)?.AssetCategoryIconPath }).ToArray();
            if (addedAssetCategories.Any())
            {
                if (await _assetCategoryRepository.CreateAsync(assetCategories: addedAssetCategories))
                    assetCategories.AddRange(addedAssetCategories);
            }

            var addedFleets = fleetNames.Where(x => fleets.Any(y => y.Name == x) != true).Select(x => new Models.Fleet() { Name = x }).ToArray();
            if (addedFleets.Any())
            {
                if (await _fleetRepository.CreateAsync(fleets: addedFleets))
                    fleets.AddRange(addedFleets);
            }

            foreach (var update in updates)
            {
                var asset = assets.FirstOrDefault(a => a.Id == update.AssetId);
                if (asset == null && !string.IsNullOrEmpty(update.AssetName) && !string.IsNullOrEmpty(update.AssetCategory))
                {
                    var assetCategory = assetCategories.FirstOrDefault(ac => ac.Name == update.AssetCategory);
                    var fleet = fleets.FirstOrDefault(f => f.Name == update.Fleet);

                    asset = new Asset
                    {
                        Name = update.AssetName,
                        AssetCategory = assetCategory,
                        AssetLogItems = new List<AssetLogItem>(),
                        AssetFleets = new List<AssetFleet>()
                    };

                    if (fleet != null)
                    {
                        asset.AssetFleets.Add(new AssetFleet()
                        {
                            AssetId = asset.Id,
                            Asset = asset,
                            FleetId = fleet.Id,
                            Fleet = fleet,
                            CreatedOnUtc = DateTime.UtcNow
                        });
                    }

                    await _assetRepository.CreateAsync(asset);
                }
                else
                {
                    // No asset ID, and no name and type, so we just skip since we don't know what this is
                    continue;
                }

                if (update.LocationLatitude.HasValue && update.LocationLongitude.HasValue && update.LocationTimestamp.HasValue)
                {
                    var assetLogItem = new AssetLogItem
                    {
                        FileId = update.FileId,
                        AssetId = asset.Id,
                        Asset = asset,
                        Location = new Location()
                        {
                            Latitude = update.LocationLatitude.Value,
                            Longitude = update.LocationLongitude.Value,
                            Timestamp = update.LocationTimestamp.Value
                        }
                    };

                    await _assetLogItemRepository.CreateAsync(assetLogItem);
                }
            }

            var response = new UpdateAssetLogsResponse
            {

            };

            return response;
        }

        private async Task<GetAssetsResponse> GetAssetsByFleetId(int? fleetId)
        {
            var assets = await _assetRepository.GetAsync(v => v.AssetFleets.Any(vf => vf.FleetId == fleetId));
            var viewModels = assets.Select(v => new AssetViewModel
            {
                Id = v.Id,
                Name = v.Name,
                LastKnownLocation = v.AssetLogItems?.LastOrDefault()?.Location
            });

            var response = new GetAssetsResponse
            {
                Assets = viewModels
            };

            return response;
        }
    }
}
