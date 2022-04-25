using Fleet.Assets.ViewModels;

using System.Collections.Generic;

namespace Fleet.Assets.Requests
{
    public class UpdateAssetLogsRequest
    {
        public IEnumerable<AssetUpdateViewModel> Updates { get; set; }
    }
}