using CologneStore.Models;

namespace CologneStore.Repositories
{
    public interface ICologneRepository
    {
        Task AddCologne(Cologne cologne);
        Task DeleteCologne(Cologne cologne);
        Task<Cologne?> GetCologneById(int id);
        Task<IEnumerable<Cologne>> GetColognes();
        Task UpdateCologne(Cologne book);
    }
}
