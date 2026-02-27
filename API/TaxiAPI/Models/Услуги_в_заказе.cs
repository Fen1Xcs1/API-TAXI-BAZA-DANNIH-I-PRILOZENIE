using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaxiAPI.Models
{
    [Table("Услуги_в_заказе")]
    public class Услуги_в_заказе
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("услуга_в_заказе_id")]
        public int услуга_в_заказе_id { get; set; }

        [Required]
        [Column("заказ_id")]
        public int заказ_id { get; set; }

        [Required]
        [Column("услуга_id")]
        public int услуга_id { get; set; }

        // Навигационные свойства
        [ForeignKey("заказ_id")]
        public virtual Заказы Заказ { get; set; }

        [ForeignKey("услуга_id")]
        public virtual Услуги Услуга { get; set; }
    }
}