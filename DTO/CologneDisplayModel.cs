using CologneStore.Models;
using Humanizer.Localisation;

namespace CologneStore.DTO
{
    public class CologneDisplayModel
    {
        public IEnumerable<Cologne> Colognes { get; set; }
        public IEnumerable<CologneType> Types { get; set; }
        public IEnumerable<CologneFor> ColognesFor { get; set; }
    }
}
