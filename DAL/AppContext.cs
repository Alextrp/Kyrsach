using DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
  
    public class AppContext:DbContext
    {
        public DbSet<Cargo> Cargos { get; set; }
        public DbSet<CargoType> CargoTypes { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<VehicleType> VehicleTypes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Tracking> Trackings { get; set; }
        public DbSet<User> Users { get; set; }

        public AppContext(DbContextOptions<AppContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Настройка внешних ключей и связей между таблицами
            modelBuilder.Entity<Cargo>()
                .HasOne(c => c.CargoType)
                .WithMany()
                .HasForeignKey(c => c.CargoTypeID);

            modelBuilder.Entity<Cargo>()
                .HasOne(c => c.Owner)
                .WithMany()
                .HasForeignKey(c => c.UserID);

            modelBuilder.Entity<Vehicle>()
                .HasOne(v => v.VehicleType)
                .WithMany()
                .HasForeignKey(v => v.VehicleTypeID);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Cargo)
                .WithMany()
                .HasForeignKey(o => o.CargoID);
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Driver)
                .WithMany()
                .HasForeignKey(o => o.UserID);
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Status)
                .WithMany()
                .HasForeignKey(o => o.StatusID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Order)
                .WithMany()
                .HasForeignKey(p => p.OrderID);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.Order)
                .WithMany()
                .HasForeignKey(r => r.OrderID);
            modelBuilder.Entity<Review>()
                .HasOne(r => r.Customer)
                .WithMany()
                .HasForeignKey(r => r.UserID);
            modelBuilder.Entity<Session>()
                .HasOne(s => s.User)
                .WithMany()
                .HasForeignKey(s => s.UserId);
            modelBuilder.Entity<Tracking>()
                .HasOne(t => t.Status)
                .WithMany()
                .HasForeignKey(t => t.StatusID);


            // Здесь можно добавить дополнительные настройки моделей
        }
    }
}
