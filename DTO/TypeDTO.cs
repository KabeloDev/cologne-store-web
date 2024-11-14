using System.ComponentModel.DataAnnotations;

namespace CologneStore.DTO
{
    public class TypeDTO
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(40)]
        public string TypeName { get; set; }
    }
}
