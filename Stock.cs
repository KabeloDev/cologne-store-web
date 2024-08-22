using System.ComponentModel.DataAnnotations.Schema;

namespace CologneStore.Models
{
    public class Stock
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int CologneId { get; set; }
        public Cologne Cologne { get; set; }
    }
}
