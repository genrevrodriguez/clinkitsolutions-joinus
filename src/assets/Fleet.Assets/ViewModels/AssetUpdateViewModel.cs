using Fleet.Common;
using Fleet.Assets.Models;
using System;

namespace Fleet.Assets.ViewModels
{
    public class AssetUpdateViewModel
    {
        public int? AssetId { get; set; }
        public string AssetName { get; set; }

        public string AssetCategory { get; set; }
        public string AssetCategoryIconPath { get; set; }

        public string Fleet { get; set; }

        public string FileId { get; set; }

        public double? LocationLatitude { get; set; }
        public double? LocationLongitude { get; set; }
        public DateTime? LocationTimestamp { get; set; }
    }
}
