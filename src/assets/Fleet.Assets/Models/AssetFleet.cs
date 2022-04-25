using System;

namespace Fleet.Assets.Models
{
    public class AssetFleet
    {
        public int AssetId { get; set; }
        public Asset Asset { get; set; }
        public int FleetId { get; set; }
        public Fleet Fleet { get; set; }
        public DateTime CreatedOnUtc { get; set; }
    }
}
