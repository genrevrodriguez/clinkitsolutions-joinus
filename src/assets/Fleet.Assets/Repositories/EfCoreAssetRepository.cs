using Fleet.Assets.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Fleet.Assets.Repositories
{
    public class EfCoreAssetRepository : IAssetRepository
    {
        private readonly AssetDbContext _database;

        public EfCoreAssetRepository(AssetDbContext database)
        {
            _database = database;
        }

        public async Task CreateAsync(Asset asset)
        {
            _database.Assets.Add(asset);

            await _database.SaveChangesAsync();
        }

        public async Task<Asset> GetAsync(int id)
        {
            return await _database.Assets
                .Where(a => a.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Asset>> GetAsync()
        {
            return await _database.Assets
                .Where(a => a.AssetLogItems.Any())
                .Include(a => a.AssetCategory)
                .Include(a => a.AssetLogItems.OrderBy(l => l.Location.Timestamp))
                .ToListAsync();
        }


        public async Task<IEnumerable<Asset>> GetAsync(Expression<Func<Asset, bool>> filter)
        {
            return await _database.Assets
                .Where(filter)
                .Where(a => a.AssetLogItems.Any())
                .Include(a => a.AssetCategory)
                .Include(a => a.AssetLogItems.OrderBy(l => l.Location.Timestamp))
                .ToListAsync();
        }
    }
}
