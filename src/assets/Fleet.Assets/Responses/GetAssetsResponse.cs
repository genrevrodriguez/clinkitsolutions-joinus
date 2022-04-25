using Fleet.Assets.ViewModels;
using System.Collections.Generic;

namespace Fleet.Assets.Responses
{
    public class GetAssetsResponse
    {
        public IEnumerable<AssetViewModel> Assets { get; set; }
    }
}
