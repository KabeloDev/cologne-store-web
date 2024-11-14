using CologneStore.Constants;
using CologneStore.DTO;
using CologneStore.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CologneStore.Controllers
{
    [Authorize(Roles = nameof(Roles.Admin))]
    public class StockController : Controller
    {
        private readonly IStockRepository _stockRepository;

        public StockController(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }

        public async Task<IActionResult> Index(string sTerm = "")
        {
            var stocks = await _stockRepository.GetStocks(sTerm);
            return View(stocks);
        }

		public async Task<IActionResult> ManageStock(int cologneId)
		{
			var existingStock = await _stockRepository.GetStockByCologneId(cologneId);
			var stock = new StockDTO
			{
				CologneId = cologneId,
				Quantity = existingStock != null ? existingStock.Quantity : 0,
			};

			return View(stock);
		}

		[HttpPost]
		public async Task<IActionResult> ManageStock(StockDTO stock)
		{
			if (!ModelState.IsValid)
			{
				return View(stock);
			}

			try
			{
				await _stockRepository.ManageStock(stock);
				TempData["successMessage"] = "Stock is updated successfully!";
			}
			catch (Exception)
			{

				TempData["errorMessage"] = "Stock is not updated.";
			}

			return RedirectToAction("Index");
		}
	}
}

