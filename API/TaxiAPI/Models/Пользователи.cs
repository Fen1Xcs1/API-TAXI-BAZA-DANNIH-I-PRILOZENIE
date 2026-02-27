using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaxiAPI.Models
{
    [Table("Пользователи")]
    public class Пользователи
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("пользователи_id")]
        public int пользователи_id { get; set; }

        [Required]
        [Column("ФИО")]
        [StringLength(100)]
        public string ФИО { get; set; }

        [Required]
        [Column("номер_телефона")]
        [StringLength(20)]
        public string номер_телефона { get; set; }

        [Required]
        [Column("статус")]
        [StringLength(20)]
        public string статус { get; set; }

        [Required]
        [Column("логин")]
        [StringLength(50)]
        public string логин { get; set; }

        [Required]
        [Column("пароль")]
        [StringLength(255)]
        public string пароль { get; set; }

        [Required]
        [Column("дата_регистрации")]
        public DateTime дата_регистрации { get; set; }

        // Навигационные свойства
        public virtual ICollection<Заказы> Заказы { get; set; }
        public virtual ICollection<Уведомления> Уведомления { get; set; }
    }
}