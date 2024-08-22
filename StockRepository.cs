using CologneStore.Data;
using CologneStore.DTO;
using CologneStore.Models;
using Microsoft.EntityFrameworkCore;

namespace CologneStore.Repositories
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDbContext _context;

        public StockRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Stock?> GetStockByCologneId(int CologneId)
        {
            return await _context.Stocks.FirstOrDefaultAsync(a => a.CologneId == CologneId);
        }

        public async Task<IEnumerable<StockDisplayModel>> GetStocks(string sTerm = "")
        {
            var stocks = await (from cologne in _context.Colognes
                                join stock in _context.Stocks on cologne.Id
                                equals stock.CologneId into cologne_stock
                                from cologneStock in cologne_stock.DefaultIfEmpty()
                                where string.IsNullOrEmpty(sTerm) || cologne.CologneName.ToLower().Contains(sTerm.ToLower())
                                select new StockDisplayModel
                                {
                                    CologneId = cologne.Id,
                                    CologneName = cologne.CologneName,
                                    Quantity = cologneStock == null ? 0 : cologneStock.Quantity,
                                }).ToListAsync();

            return stocks;
        }

		public async Task ManageStock(StockDTO stockToManage)
		{
			var existingStock = await GetStockByCologneId(stockToManage.CologneId);
			if (existingStock == null)
			{
				var stock = new Stock
				{
					CologneId = stockToManage.CologneId,
					Quantity = stockToManage.Quantity,
				};
				_context.Stocks.Add(stock);
			}
			else
			{
				existingStock.Quantity = stockToManage.Quantity;
			}

			await _context.SaveChangesAsync();
		}
	}
}
