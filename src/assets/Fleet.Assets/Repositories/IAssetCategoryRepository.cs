using Fleet.Assets.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Fleet.Assets.Repositories
{
    public interface IAssetCategoryRepository
    {
        Task<IEnumerable<AssetCategory>> GetAsync();
        Task<IEnumerable<AssetCategory>> GetAsync(Expression<Func<AssetCategory, bool>> filter);
        Task<bool> CreateAsync(params AssetCategory[] assetCategories);
    }
}
