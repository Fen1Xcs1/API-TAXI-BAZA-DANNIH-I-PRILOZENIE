using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaxiAPI.Models
{
    [Table("Адреса")]
    public class Адреса
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("адрес_id")]
        public int адрес_id { get; set; }

        [Required]
        [Column("город")]
        [StringLength(50)]
        public string город { get; set; }

        [Required]
        [Column("улица")]
        [StringLength(100)]
        public string улица { get; set; }

        [Required]
        [Column("дом")]
        [StringLength(10)]
        public string дом { get; set; }

        [Column("подъезд")]
        [StringLength(10)]
        public string? подъезд { get; set; }

        [Column("квартира")]
        [StringLength(10)]
        public string? квартира { get; set; }

        [Required]
        [Column("начальный_адрес")]
        public bool начальный_адрес { get; set; }

        [Required]
        [Column("конечный_адрес")]
        public bool конечный_адрес { get; set; }

        // Навигационные свойства
        public virtual ICollection<Заказы> ЗаказыОтправления { get; set; }
        public virtual ICollection<Заказы> ЗаказыНазначения { get; set; }
    }
}