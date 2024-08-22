using System.ComponentModel.DataAnnotations;

namespace CologneStore.Models
{
    public class Cart
    {
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        public bool IsDeleted { get; set; } = false;
        public ICollection<CartDetail> CartDetails { get; set; }
    }
}
