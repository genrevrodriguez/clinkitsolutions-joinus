using Fleet.Assets.Requests;
using Fleet.Assets.Responses;
using Fleet.Assets.Services;
using Microsoft.AspNetCore.Mvc;

namespace Fleet.Api.Controllers
{
    [ApiController]
    [Route("api/assetcategories")]
    public class AssetCategoriesController : Controller
    {
        private readonly IAssetCategoryService _assetCategoryService;

        public AssetCategoriesController(IAssetCategoryService assetCategoryService)
        {
            _assetCategoryService = assetCategoryService;
        }

        /// <summary>
        /// Gets a list of AssetCategories
        /// </summary>
        /// <param name="request"></param>
        /// <returns>A list of AssetCategories</returns>
        [HttpGet]
        [Route("")]
        public Task<GetAssetCategoriesResponse> GetAssetCategoriesAsync([FromQuery] GetAssetCategoriesRequest request) => _assetCategoryService.GetAssetCategoriesAsync(request);
    }
}
