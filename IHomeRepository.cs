using CologneStore.Models;
using Humanizer.Localisation;

namespace CologneStore.Repositories
{
    public interface IHomeRepository
    {
        Task<IEnumerable<Cologne>> GetColognes(string sTerm = "", int typeId = 0, int cologneForId = 0);
        Task<IEnumerable<CologneType>> Types();
        Task<IEnumerable<CologneFor>> ColognesFor();
    }
}
