using CologneStore.Data;
using CologneStore.Models;
using Humanizer.Localisation;
using Microsoft.EntityFrameworkCore;

namespace CologneStore.Repositories
{
    public class HomeRepository : IHomeRepository
    {
        private readonly ApplicationDbContext _context;

        public HomeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CologneType>> Types()
        {
            return await _context.Types.ToListAsync();
        }

        public async Task<IEnumerable<CologneFor>> ColognesFor()
        {
            return await _context.ColognesFor.ToListAsync();
        }

        public async Task<IEnumerable<Cologne>> GetColognes(string sTerm = "", int typeId = 0, int cologneForId = 0)
        {
            sTerm = sTerm.ToLower();
            IEnumerable<Cologne> colognes = await (from cologne in _context.Colognes
                                                   join type in _context.Types on cologne.TypeId
                                                   equals type.Id
                                                   join stock in _context.Stocks on cologne.Id equals stock.CologneId
                                                   into cologneStocks
                                                   from cologneWithStock in cologneStocks.DefaultIfEmpty()
                                                   where string.IsNullOrWhiteSpace(sTerm) || (type != null && cologne.CologneName.ToLower().StartsWith(sTerm))
                                                   select new Cologne
                                                   {
                                                       Id = cologne.Id,
                                                       CologneImage = cologne.CologneImage,
                                                       CologneMakerName = cologne.CologneMakerName,
                                                       TypeId = cologne.TypeId,
                                                       CologneForId = cologne.CologneForId,
                                                       Price = cologne.Price,
                                                       CologneName = cologne.Type.TypeName,
                                                       Quantity = cologneWithStock == null ? 0 : cologneWithStock.Quantity,
                                                   }).ToListAsync();

            if (typeId > 0)
            {
                colognes = colognes.Where(a => a.TypeId == typeId).ToList();
                return colognes;
            }

            if (cologneForId > 0)
            {
                colognes = colognes.Where(a => a.CologneForId == cologneForId).ToList();
                return colognes;
            }

            if (typeId > 0 && cologneForId > 0)
            {
                colognes = colognes.Where(a => a.TypeId == typeId).Where(a => a.CologneForId == cologneForId).ToList();
                return colognes;
            }

            return colognes;
        }
    }
}
