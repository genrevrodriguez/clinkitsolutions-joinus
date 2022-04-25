using Fleet.Assets.Models;
using System.Threading.Tasks;

namespace Fleet.Assets.Repositories
{
    public interface IAssetLogItemRepository
    {
        Task CreateAsync(AssetLogItem item);
    }
}
