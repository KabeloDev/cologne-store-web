using CologneStore.DTO;
using CologneStore.Models;

namespace CologneStore.Repositories
{
    public interface IStockRepository
    {
        Task<IEnumerable<StockDisplayModel>> GetStocks(string sTerm = "");
        Task<Stock?> GetStockByCologneId(int CologneId);
        Task ManageStock(StockDTO stockToManage);
    }
}
