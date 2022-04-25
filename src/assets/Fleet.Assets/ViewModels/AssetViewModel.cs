using Fleet.Common;
using Fleet.Assets.Models;

namespace Fleet.Assets.ViewModels
{
    public class AssetViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public AssetCategory AssetCategory { get; set; }
        public Location LastKnownLocation { get; set; }
    }
}
