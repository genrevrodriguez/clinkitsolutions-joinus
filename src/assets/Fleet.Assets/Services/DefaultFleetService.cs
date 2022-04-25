using Fleet.Assets.Repositories;
using Fleet.Assets.Requests;
using Fleet.Assets.Responses;
using Fleet.Assets.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace Fleet.Assets.Services
{
    public class DefaultFleetService : IFleetService
    {
        private readonly IFleetRepository _fleetRepository;

        public DefaultFleetService(IFleetRepository fleetRepository)
        {
            _fleetRepository = fleetRepository;
        }

        public async Task<GetFleetsResponse> GetFleetsAsync(GetFleetsRequest request)
        {
            var fleets = await _fleetRepository.GetAsync();
            var response = new GetFleetsResponse
            {
                Fleets = fleets.Select(f => new FleetViewModel
                {
                    Id = f.Id,
                    Name = f.Name
                })
            };

            return response;
        }
    }
}
