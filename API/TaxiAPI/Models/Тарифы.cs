using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaxiAPI.Models
{
    [Table("Тарифы")]
    public class Тарифы
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("тариф_id")]
        public int тариф_id { get; set; }

        [Required]
        [Column("название")]
        [StringLength(50)]
        public string название { get; set; }

        [Required]
        [Column("цена")]
        public decimal цена { get; set; }

        [Column("описание")]
        [StringLength(255)]
        public string? описание { get; set; }

        // Навигационное свойство
        public virtual ICollection<Автомобили> Автомобили { get; set; }
    }
}