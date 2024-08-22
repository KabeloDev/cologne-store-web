using System.ComponentModel.DataAnnotations;

namespace CologneStore.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }
        [Required]
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int CologneId { get; set; }
        public Cologne Cologne { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
       
    }
}
