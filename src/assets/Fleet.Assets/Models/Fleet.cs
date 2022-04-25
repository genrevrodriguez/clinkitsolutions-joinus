using System.Collections.Generic;

namespace Fleet.Assets.Models
{
    public class Fleet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<AssetFleet> AssetFleets { get; set; }
    }
}
