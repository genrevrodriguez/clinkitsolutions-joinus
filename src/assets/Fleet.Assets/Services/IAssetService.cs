using Fleet.Assets.Requests;
using Fleet.Assets.Responses;
using System.Threading.Tasks;

namespace Fleet.Assets.Services
{
    public interface IAssetService
    {
        Task<GetAssetsResponse> GetAssetsAsync(GetAssetsRequest request);

        Task<UpdateAssetLogsResponse> UpdateAssetLogsAsync(UpdateAssetLogsRequest request);
    }
}