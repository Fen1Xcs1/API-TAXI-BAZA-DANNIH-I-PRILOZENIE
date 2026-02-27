using Microsoft.EntityFrameworkCore;
using TaxiAPI.Models;

namespace TaxiAPI.Data
{
    public class TaxiDbContext : DbContext
    {
        public TaxiDbContext(DbContextOptions<TaxiDbContext> options) : base(options)
        {
        }

        public DbSet<Автомобили> Автомобили { get; set; }
        public DbSet<Адреса> Адреса { get; set; }
        public DbSet<Заказы> Заказы { get; set; }
        public DbSet<Получение_скидки> ПолучениеСкидок { get; set; }
        public DbSet<Пользователи> Пользователи { get; set; }
        public DbSet<Скидки> Скидки { get; set; }
        public DbSet<Тарифы> Тарифы { get; set; }
        public DbSet<Уведомления> Уведомления { get; set; }
        public DbSet<Услуги> Услуги { get; set; }
        public DbSet<Услуги_в_заказе> УслугиВЗаказе { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Уникальные индексы
            modelBuilder.Entity<Автомобили>()
                .HasIndex(a => a.номерной_знак)
                .IsUnique()
                .HasDatabaseName("UQ__Автомоби__5A898D6649AFC14A");

            modelBuilder.Entity<Пользователи>()
                .HasIndex(p => p.логин)
                .IsUnique()
                .HasDatabaseName("UQ__Пользова__5EB64DCCF03002C7");

            modelBuilder.Entity<Пользователи>()
                .HasIndex(p => p.номер_телефона)
                .IsUnique()
                .HasDatabaseName("UQ__Пользова__8145ED7A85FBB63B");

            // Настройка связей с правильными именами FK
            modelBuilder.Entity<Автомобили>()
                .HasOne(a => a.Тариф)
                .WithMany(t => t.Автомобили)
                .HasForeignKey(a => a.тариф_id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Заказы>()
                .HasOne(z => z.Пользователь)
                .WithMany(p => p.Заказы)
                .HasForeignKey(z => z.пользователи_id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Заказы>()
                .HasOne(z => z.Автомобиль)
                .WithMany(a => a.Заказы)
                .HasForeignKey(z => z.автомобиль_id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Заказы>()
                .HasOne(z => z.АдресОтправления)
                .WithMany(a => a.ЗаказыОтправления)
                .HasForeignKey(z => z.адрес_отправления_id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Заказы>()
                .HasOne(z => z.АдресНазначения)
                .WithMany(a => a.ЗаказыНазначения)
                .HasForeignKey(z => z.адрес_назначения_id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Получение_скидки>()
                .HasOne(p => p.Заказ)
                .WithMany(z => z.ПолученныеСкидки)
                .HasForeignKey(p => p.заказ_id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Получение_скидки>()
                .HasOne(p => p.Скидка)
                .WithMany(s => s.ПолучениеСкидок)
                .HasForeignKey(p => p.скидка_id)
                .HasConstraintName("FK__Получение__скидк__60A75C0F")
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Услуги_в_заказе>()
                .HasOne(u => u.Заказ)
                .WithMany(z => z.УслугиВЗаказе)
                .HasForeignKey(u => u.заказ_id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Услуги_в_заказе>()
                .HasOne(u => u.Услуга)
                .WithMany(u => u.УслугиВЗаказе)
                .HasForeignKey(u => u.услуга_id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Уведомления>()
                .HasOne(u => u.Пользователь)
                .WithMany(p => p.Уведомления)
                .HasForeignKey(u => u.пользователи_id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Уведомления>()
                .HasOne(u => u.Заказ)
                .WithMany(z => z.Уведомления)
                .HasForeignKey(u => u.заказ_id)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}