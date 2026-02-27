using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaxiAPI.Models
{
    [Table("Автомобили")]
    public class Автомобили
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("автомобиль_id")]
        public int автомобиль_id { get; set; }

        [Required]
        [Column("модель")]
        [StringLength(50)]
        public string модель { get; set; }

        [Required]
        [Column("марка")]
        [StringLength(50)]
        public string марка { get; set; }

        [Required]
        [Column("номерной_знак")]
        [StringLength(20)]
        public string номерной_знак { get; set; }

        [Required]
        [Column("цвет")]
        [StringLength(30)]
        public string цвет { get; set; }

        [Required]
        [Column("тариф_id")]
        public int тариф_id { get; set; }

        [Required]
        [Column("класс_авто")]
        [StringLength(30)]
        public string класс_авто { get; set; }

        [Required]
        [Column("активен")]
        public bool активен { get; set; }

        // Навигационные свойства
        [ForeignKey("тариф_id")]
        public virtual Тарифы Тариф { get; set; }

        public virtual ICollection<Заказы> Заказы { get; set; }
    }
}