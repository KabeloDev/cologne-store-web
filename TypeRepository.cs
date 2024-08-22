using CologneStore.Data;
using CologneStore.Models;
using Humanizer.Localisation;
using Microsoft.EntityFrameworkCore;

namespace CologneStore.Repositories
{
    public class TypeRepository : ITypeRepository
    {
        private readonly ApplicationDbContext _context;

        public TypeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddType(CologneType type)
        {
            _context.Types.Add(type);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteType(CologneType type)
        {
            _context.Types.Remove(type);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CologneType>> GetAllTypes()
        {
            return await _context.Types.ToListAsync();
        }

        public async Task<CologneType?> GetTypeById(int id)
        {
            return await _context.Types.FindAsync(id);
        }

        public async Task UpdateType(CologneType type)
        {
            _context.Types.Update(type);
            await _context.SaveChangesAsync();
        }
    }
}
