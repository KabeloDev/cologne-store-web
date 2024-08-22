using CologneStore.Data;
using CologneStore.Models;
using Microsoft.EntityFrameworkCore;

namespace CologneStore.Repositories
{
	public class ColognesForRepository : ICologneForRepository	
	{
		private readonly ApplicationDbContext _context;

		public ColognesForRepository (ApplicationDbContext context)
		{
			_context = context;
		}
		public async Task<IEnumerable<CologneFor>> GetAllColognesFor()
		{
			return await _context.ColognesFor.ToListAsync();
		}
	}
}
