using CologneStore.DTO;
using CologneStore.Models;

namespace CologneStore.Repositories
{
    public interface IUserOrderRepository
    {
        Task<IEnumerable<Order>> UserOrders(bool getAll = false);
        Task ChangeOrderStatus(UpdateOrderStatusModel data);
        Task TogglePaymentStatus(int orderId);
        Task<Order> GetOrderById(int Id);
        Task<IEnumerable<OrderStatus>> GetOrderStatus();
    }
}
