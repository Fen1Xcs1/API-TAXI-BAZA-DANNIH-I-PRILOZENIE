namespace TaxiAPI.DTO
{
    public class ЗаказDto
    {
        public int заказ_id { get; set; }
        public int пользователи_id { get; set; }
        public int? автомобиль_id { get; set; }
        public int адрес_отправления_id { get; set; }
        public int адрес_назначения_id { get; set; }
        public DateTime дата_заказа { get; set; }
        public string способ_оплаты { get; set; }
        public string статус_заказа { get; set; }
        public decimal итоговая_цена { get; set; }

        // Расширенные поля для отображения
        public ПользовательDto? Пользователь { get; set; }
        public АвтомобильDto? Автомобиль { get; set; }
        public АдресDto? АдресОтправления { get; set; }
        public АдресDto? АдресНазначения { get; set; }
        public List<СкидкаВЗаказеDto>? Скидки { get; set; }
        public List<УслугаВЗаказеDto>? Услуги { get; set; }
    }

    public class СкидкаВЗаказеDto
    {
        public int скидка_id { get; set; }
        public string название { get; set; }
        public decimal сумма_скидки { get; set; }
    }

    public class УслугаВЗаказеDto
    {
        public int услуга_id { get; set; }
        public string название { get; set; }
        public decimal цена { get; set; }
    }
}
