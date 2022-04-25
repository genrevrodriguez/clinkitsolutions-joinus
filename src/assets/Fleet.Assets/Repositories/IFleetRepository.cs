using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Fleet.Assets.Repositories
{
    public interface IFleetRepository
    {
        Task<IEnumerable<Models.Fleet>> GetAsync();
        Task<IEnumerable<Models.Fleet>> GetAsync(Expression<Func<Models.Fleet, bool>> filter);
        Task<bool> CreateAsync(params Models.Fleet[] fleets);
    }
}
