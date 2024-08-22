using CologneStore.DTO;
using CologneStore.Models;

namespace CologneStore.Repositories
{
    public interface ICartRepository
    {
        Task<int> AddItem(int cologneId, int quantity);
        Task<int> RemoveItem(int cologneId);
        Task<Cart> GetUserCart();
        Task<Cart> GetCart(string userId);
        Task<bool> DoCheckOut(CheckoutModel model);
    }
}
