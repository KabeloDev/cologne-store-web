using System.ComponentModel.DataAnnotations;

namespace CologneStore.Models
{
    public class CologneFor
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(40)]
        public string? CologneForName { get; set; }
        public List<Cologne> Colognes { get; set; }
    }
}
