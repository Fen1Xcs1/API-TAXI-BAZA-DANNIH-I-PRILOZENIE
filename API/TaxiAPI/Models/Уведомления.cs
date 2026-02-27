using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaxiAPI.Models
{
    [Table("Уведомления")]
    public class Уведомления
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("уведомление_id")]
        public int уведомление_id { get; set; }

        [Required]
        [Column("пользователи_id")]
        public int пользователи_id { get; set; }

        [Column("заказ_id")]
        public int? заказ_id { get; set; }

        [Required]
        [Column("тип_уведомления")]
        [StringLength(30)]
        public string тип_уведомления { get; set; }

        [Required]
        [Column("сообщение")]
        [StringLength(255)]
        public string сообщение { get; set; }

        [Required]
        [Column("временная_метка")]
        public DateTime временная_метка { get; set; }

        [Required]
        [Column("прочитано")]
        public bool прочитано { get; set; }

        // Навигационные свойства
        [ForeignKey("пользователи_id")]
        public virtual Пользователи Пользователь { get; set; }

        [ForeignKey("заказ_id")]
        public virtual Заказы? Заказ { get; set; }
    }
}