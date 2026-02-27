namespace TaxiAPI.DTO
{
    public class ТарифDto
    {
        public int тариф_id { get; set; }
        public string название { get; set; }
        public decimal цена { get; set; }
        public string? описание { get; set; }
    }
}
