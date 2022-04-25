using Fleet.Assets.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Fleet.Assets.Repositories
{
    public class EfCoreAssetCategoryRepository : IAssetCategoryRepository
    {
        private readonly AssetDbContext _database;

        public EfCoreAssetCategoryRepository(AssetDbContext database)
        {
            _database = database;
        }

        public async Task<IEnumerable<AssetCategory>> GetAsync()
        {
            return await _database.AssetCategories
                .Include(ac => ac.ParentAssetCategory)
                .Include(ac => ac.SubAssetCategories)
                .ToListAsync();
        }

        public async Task<IEnumerable<AssetCategory>> GetAsync(Expression<Func<AssetCategory, bool>> filter)
        {
            return await _database.AssetCategories
                .Where(filter)
                .Include(ac => ac.ParentAssetCategory)
                .Include(ac => ac.SubAssetCategories)
                .ToListAsync();
        }

        public async Task<bool> CreateAsync(params AssetCategory[] assetCategories)
        {
            if (assetCategories?.Any() != true) return false;

            await _database.AddRangeAsync(entities: assetCategories);

            return await _database.SaveChangesAsync() > 0;
        }
    }
}
