using Fleet.Assets.ViewModels;
using System.Collections.Generic;

namespace Fleet.Assets.Responses
{
    public class GetAssetCategoriesResponse
    {
        public IEnumerable<AssetCategoryViewModel> AssetCategories { get; set; }
    }
}