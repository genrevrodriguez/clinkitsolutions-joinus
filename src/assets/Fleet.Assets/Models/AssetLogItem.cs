using Fleet.Common;
using System;

namespace Fleet.Assets.Models
{
    public class AssetLogItem
    {
        public int Id { get; set; }

        // used as reference for tusdotnet file id
        public string FileId { get; set; }

        public int? AssetId { get; set; }
        public Asset Asset { get; set; }
        public int? LocationId { get; set; }
        public Location Location { get; set; }
    }
}
