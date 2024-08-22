using CologneStore.Models;
using Humanizer.Localisation;

namespace CologneStore.Repositories
{
    public interface ITypeRepository
    {
        Task AddType(CologneType type);
        Task UpdateType(CologneType type);
        Task DeleteType(CologneType type);
        Task<CologneType?> GetTypeById(int id);
        Task<IEnumerable<CologneType>> GetAllTypes();
    }
}
