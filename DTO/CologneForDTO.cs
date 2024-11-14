using System.ComponentModel.DataAnnotations;

namespace CologneStore.DTO
{
    public class CologneForDTO
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(40)]
        public string CologneForName { get; set; }
    }
}
