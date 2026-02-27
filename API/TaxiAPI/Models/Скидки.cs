using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaxiAPI.Models
{
    [Table("Скидки")]
    public class Скидки
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("скидка_id")]
        public int скидка_id { get; set; }

        [Required]
        [Column("название")]
        [StringLength(50)]
        public string название { get; set; }

        [Required]
        [Column("процент_скидки")]
        public decimal процент_скидки { get; set; }

        [Required]
        [Column("описание")]
        [StringLength(255)]
        public string описание { get; set; }

        [Required]
        [Column("дата_начала")]
        public DateTime дата_начала { get; set; }

        [Required]
        [Column("дата_окончания")]
        public DateTime дата_окончания { get; set; }

        [Required]
        [Column("активна")]
        public bool активна { get; set; }

        // Навигационное свойство
        public virtual ICollection<Получение_скидки> ПолучениеСкидок { get; set; }
    }
}