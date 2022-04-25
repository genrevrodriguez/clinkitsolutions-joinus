using Fleet.Assets.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Fleet.Assets.Repositories
{
    public interface IAssetRepository
    {
        Task CreateAsync(Asset asset);
        Task<Asset> GetAsync(int id);
        Task<IEnumerable<Asset>> GetAsync();
        Task<IEnumerable<Asset>> GetAsync(Expression<Func<Asset,bool>> filter);
    }
}
