using System.ComponentModel.DataAnnotations;

namespace CologneStore.Models
{
    public class CologneType
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(40)]
        public string TypeName { get; set; }
        public List<Cologne> Colognes { get; set; }
    }
}
