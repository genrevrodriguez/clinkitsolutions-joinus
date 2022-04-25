using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fleet.Assets.Models
{
    public class AssetCategory
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string IconPath { get; set; }

        public int? ParentAssetCategoryId { get; set; }
        public AssetCategory ParentAssetCategory { get; set; }
        public virtual ICollection<AssetCategory> SubAssetCategories { get; set; }
    }
}
