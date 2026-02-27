using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaxiAPI.Models
{
    [Table("Услуги")]
    public class Услуги
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("услуга_id")]
        public int услуга_id { get; set; }

        [Required]
        [Column("название")]
        [StringLength(100)]
        public string название { get; set; }

        [Required]
        [Column("цена")]
        public decimal цена { get; set; }

        [Column("описание")]
        [StringLength(255)]
        public string? описание { get; set; }

        [Required]
        [Column("активна")]
        public bool активна { get; set; }

        // Навигационное свойство
        public virtual ICollection<Услуги_в_заказе> УслугиВЗаказе { get; set; }
    }
}