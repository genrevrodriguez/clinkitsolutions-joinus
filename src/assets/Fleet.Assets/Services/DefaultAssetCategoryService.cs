using Fleet.Assets.Repositories;
using Fleet.Assets.Requests;
using Fleet.Assets.Responses;
using Fleet.Assets.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace Fleet.Assets.Services
{
    public class DefaultAssetCategoryService : IAssetCategoryService
    {
        private readonly IAssetCategoryRepository _assetCategoryRepository;

        public DefaultAssetCategoryService(IAssetCategoryRepository assetCategoryRepository)
        {
            _assetCategoryRepository = assetCategoryRepository;
        }

        public async Task<GetAssetCategoriesResponse> GetAssetCategoriesAsync(GetAssetCategoriesRequest request)
        {
            var assetCategories = await _assetCategoryRepository.GetAsync();
            var response = new GetAssetCategoriesResponse
            {
                AssetCategories = assetCategories.Select(ac => new AssetCategoryViewModel
                {
                    Id = ac.Id,
                    Name = ac.Name,
                    IconPath = ac.IconPath
                })
            };

            return response;
        }
    }
}
