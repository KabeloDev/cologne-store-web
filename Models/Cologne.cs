using Humanizer.Localisation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CologneStore.Models
{
    public class Cologne
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(40)]
        public string CologneName { get; set; }
        [Required]
        [MaxLength(40)]
        public string CologneMakerName { get; set; }
        public string? CologneImage { get; set; }
        [Required]
        public int TypeId { get; set; }
        public CologneType Type { get; set; }
        [Required]
        public int CologneForId { get; set; }
        public CologneFor CologneFor { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public List<OrderDetail> OrderDetail { get; set; }
        public List<CartDetail> CartDetail { get; set; }
        public Stock Stock { get; set; }

        [NotMapped]
        public string TypeName { get; set; }
    }
}
