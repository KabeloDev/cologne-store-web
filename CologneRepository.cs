using CologneStore.Data;
using CologneStore.Models;
using Microsoft.EntityFrameworkCore;

namespace CologneStore.Repositories
{
    public class CologneRepository : ICologneRepository
    {
        private readonly ApplicationDbContext _context;

        public CologneRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddCologne(Cologne cologne)
        {
            _context.Colognes.Add(cologne);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCologne(Cologne cologne)
        {
            _context.Colognes.Remove(cologne);
            await _context.SaveChangesAsync();
        }

        public async Task<Cologne?> GetCologneById(int id)
        {
            return await _context.Colognes.FindAsync(id);
        }

        public async Task<IEnumerable<Cologne>> GetColognes()
        {
            return await _context.Colognes.Include(a => a.Type).Include(a => a.CologneFor).ToListAsync();
        }

        public async Task UpdateCologne(Cologne cologne)
        {
            _context.Colognes.Update(cologne);
            await _context.SaveChangesAsync();
        }
    }
}
