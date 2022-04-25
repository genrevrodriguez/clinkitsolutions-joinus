using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Fleet.Assets.Repositories
{
    public class EfCoreFleetRepository : IFleetRepository
    {
        private readonly AssetDbContext _database;

        public EfCoreFleetRepository(AssetDbContext database)
        {
            _database = database;
        }

        public async Task<IEnumerable<Models.Fleet>> GetAsync()
        {
            return await _database.Fleets.ToListAsync();
        }

        public async Task<IEnumerable<Models.Fleet>> GetAsync(Expression<Func<Models.Fleet, bool>> filter)
        {
            return await _database.Fleets
                .Where(filter)
                .ToListAsync();
        }

        public async Task<bool> CreateAsync(params Models.Fleet[] fleets)
        {
            if (fleets?.Any() != true) return false;

            await _database.AddRangeAsync(entities: fleets);

            return await _database.SaveChangesAsync() > 0;
        }
    }
}
