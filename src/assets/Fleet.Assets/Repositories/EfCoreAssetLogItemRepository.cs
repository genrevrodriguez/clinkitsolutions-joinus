using Fleet.Assets.Models;
using System.Threading.Tasks;

namespace Fleet.Assets.Repositories
{
    public class EfCoreAssetLogItemRepository : IAssetLogItemRepository
    {
        private readonly AssetDbContext _database;

        public EfCoreAssetLogItemRepository(AssetDbContext database)
        {
            _database = database;
        }

        public async Task CreateAsync(AssetLogItem item)
        {
            _database.AssetLogItems.Add(item);
            _database.Assets.Attach(item.Asset);
            await _database.SaveChangesAsync();
        }
    }
}
