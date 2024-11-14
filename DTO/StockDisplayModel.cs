namespace CologneStore.DTO
{
    public class StockDisplayModel
    {
        public int Id { get; set; }
        public int CologneId { get; set; }
        public int Quantity { get; set; }
        public string? CologneName { get; set; }
    }
}
