namespace TaxiAPI.DTO
{
    public class АвтомобильDto
    {
        public int автомобиль_id { get; set; }
        public string модель { get; set; }
        public string марка { get; set; }
        public string номерной_знак { get; set; }
        public string цвет { get; set; }
        public int тариф_id { get; set; }
        public string класс_авто { get; set; }
        public bool активен { get; set; }
        public ТарифDto? Тариф { get; set; }
    }
}
