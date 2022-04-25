using Fleet.Assets.ViewModels;
using System.Collections.Generic;

namespace Fleet.Assets.Responses
{
    public class GetFleetsResponse
    {
        public IEnumerable<FleetViewModel> Fleets { get; set; }
    }
}