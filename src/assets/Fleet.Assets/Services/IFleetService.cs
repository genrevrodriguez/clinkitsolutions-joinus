using Fleet.Assets.Requests;
using Fleet.Assets.Responses;
using System.Threading.Tasks;

namespace Fleet.Assets.Services
{
    public interface IFleetService
    {
        Task<GetFleetsResponse> GetFleetsAsync(GetFleetsRequest request);
    }
}
