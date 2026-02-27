namespace TaxiAPI.DTO
{
    public class АдресDto
    {
        public int адрес_id { get; set; }
        public string город { get; set; }
        public string улица { get; set; }
        public string дом { get; set; }
        public string? подъезд { get; set; }
        public string? квартира { get; set; }
        public bool начальный_адрес { get; set; }
        public bool конечный_адрес { get; set; }
    }
}
