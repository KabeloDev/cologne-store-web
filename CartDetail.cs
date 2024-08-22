using System.ComponentModel.DataAnnotations;

namespace CologneStore.Models
{
    public class CartDetail
    {
        public int Id { get; set; }
        [Required]
        public int CartId { get; set; }
        public Cart Cart { get; set; }
        public int CologneId { get; set; }
        public Cologne Cologne { get; set; }
        public int Quantity { get; set; }
        [Required]
        public double UnitPrice { get; set; }
    }
}
