using CologneStore.DTO;
using CologneStore.Repositories;
using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Mvc;

namespace CologneStore.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartRepository _cartRepository;

        public CartController(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        

        public async Task<IActionResult> AddItem(int cologneId, int quantity = 1, int redirect = 0)
        {
            var cartCount = await _cartRepository.AddItem(cologneId, quantity);
            if (redirect == 0)
                return Ok(cartCount);

            return RedirectToAction("GetUserCart");

        }

        public async Task<IActionResult> RemoveItem(int cologneId)
        {
            var cartCount = await _cartRepository.RemoveItem(cologneId);
            return RedirectToAction("GetUserCart");
        }
        public async Task<IActionResult> GetUserCart()
        {
            var cart = await _cartRepository.GetUserCart();
            return View(cart);
        }

        [HttpGet]
        public IActionResult Checkout()
        {
            return View();
        }

        public async Task<IActionResult> Checkout(CheckoutModel checkout)
        {
            bool isCheckedOut = await _cartRepository.DoCheckOut(checkout);
            if (!ModelState.IsValid)
                return View(checkout);
            if (isCheckedOut == false)
            {
				TempData["errorMessage"] = "Payment failed.";
				return View(checkout);
			}
            else
            {
                TempData["successMessage"] = "Payment successful!";
				return RedirectToAction("GetUserCart");
			}
                


        }

      
    }
}
