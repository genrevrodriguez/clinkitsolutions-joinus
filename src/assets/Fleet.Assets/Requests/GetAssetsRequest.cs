namespace Fleet.Assets.Requests
{
    public class GetAssetsRequest
    {
        public int? FleetId { get; set; }
        public int? AssetCategoryId { get; set; }
        public string FileId { get; set; }
    }
}
