using CologneStore.Models;

namespace CologneStore.DTO
{
    public class OrderDetailModel
    {
        public string DivId { get; set; }
        public IEnumerable<OrderDetail> OrderDetails { get; set; }
    }
}
