using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaxiAPI.Models
{
    [Table("Получение_скидки")]
    public class Получение_скидки
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("получ_скидки_id")]
        public int получ_скидки_id { get; set; }

        [Required]
        [Column("заказ_id")]
        public int заказ_id { get; set; }

        [Required]
        [Column("скидка_id")]
        public int скидка_id { get; set; }

        [Required]
        [Column("сумма_скидки")]
        public decimal сумма_скидки { get; set; }

        // Навигационные свойства
        [ForeignKey("заказ_id")]
        public virtual Заказы Заказ { get; set; }

        [ForeignKey("скидка_id")]
        public virtual Скидки Скидка { get; set; }
    }
}