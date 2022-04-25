using Fleet.Common;
using System.Collections.Generic;

namespace Fleet.Assets.Models
{
    public class Asset
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int? AssetCategoryId { get; set; }
        public AssetCategory AssetCategory { get; set; }

        public virtual ICollection<AssetLogItem> AssetLogItems { get; set; }
        public virtual ICollection<AssetFleet> AssetFleets { get; set; }
    }
}
