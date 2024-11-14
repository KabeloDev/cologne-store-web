using System.ComponentModel.DataAnnotations;

namespace CologneStore.DTO
{
    public class StockDTO
    {
        public int CologneId { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be a non-negative value.")]
        public int Quantity { get; set; }
    }
}
