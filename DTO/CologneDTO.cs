using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CologneStore.DTO
{
    public class CologneDTO
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(40)]
        public string CologneName { get; set; }
        public int TypeId { get; set; }
        public int CologneForId { get; set; }
        [Required]
        [MaxLength(40)]
        public string CologneMakerName { get; set; }
        public string? CologneImage { get; set; }
        [Required]
        public double Price { get; set; }
        public IFormFile? ImageFile { get; set; }
        public IEnumerable<SelectListItem> TypeList { get; set; }
        public IEnumerable<SelectListItem> CologneForList { get; set; }
    }
}
