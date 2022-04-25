using Fleet.Assets.Requests;
using Fleet.Assets.Responses;
using Fleet.Assets.Services;
using Microsoft.AspNetCore.Mvc;

namespace Fleet.Api.Controllers
{
    [ApiController]
    [Route("api/assets")]
    public class AssetsController : Controller
    {
        private readonly IAssetService _assetService;

        public AssetsController(IAssetService assetService)
        {
            _assetService = assetService;
        }

        /// <summary>
        /// Get a list of vehicles optionally filtered by fleet ID
        /// </summary>
        /// <param name="request">The optional fleet ID</param>
        /// <returns>A list of vehicles</returns>
        [Route("")]
        [HttpGet]
        public Task<GetAssetsResponse> GetAssetsAsync([FromQuery] GetAssetsRequest request) => _assetService.GetAssetsAsync(request);

        /// <summary>
        /// Update vehicle location logs
        /// </summary>
        /// <param name="request"></param>
        /// <returns>An empty response</returns>
        [Route("logs")]
        [HttpPost]
        public Task<UpdateAssetLogsResponse> UpdateAssetLogsAsync([FromBody] UpdateAssetLogsRequest request) => _assetService.UpdateAssetLogsAsync(request);
    }
}
