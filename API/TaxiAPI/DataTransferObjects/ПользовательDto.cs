namespace TaxiAPI.DTO
{
    public class ПользовательDto
    {
        public int пользователи_id { get; set; }
        public string ФИО { get; set; }
        public string номер_телефона { get; set; }
        public string статус { get; set; }
        public string логин { get; set; }
        public DateTime дата_регистрации { get; set; }
    }
}
