using DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
  
    public class AppContext: IdentityDbContext<User>
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

        public DbSet<Client> Client { get; set; }
        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<Manager> Managers { get; set; }

        public AppContext(DbContextOptions<AppContext> options)
            : base(options)
        {
            //Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=TransportFirm;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Client>().HasNoKey().ToView("Clients");
            modelBuilder.Entity<Administrator>().HasNoKey().ToView("Administrators");
            modelBuilder.Entity<Manager>().HasNoKey().ToView("Managers");

            modelBuilder.Entity<User>().HasKey(u => u.Id);


            modelBuilder.Entity<Order>()
                .HasOne(o => o.Cargo)
                .WithMany()
                .HasForeignKey(o => o.CargoID)
                .OnDelete(DeleteBehavior.Restrict); 
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Status) 
                .WithOne()
                .HasForeignKey<OrderStatus>(o => o.StatusID);
            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithOne()
                .HasForeignKey<Order>(u => u.UserID);

           

            modelBuilder.Entity<Cargo>()
                .HasOne(c => c.CargoType)
                .WithMany()
                .HasForeignKey(c => c.CargoTypeID);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.Order)
                .WithOne()
                .HasForeignKey<Order>(r => r.OrderID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Tracking>()
                .HasOne(r => r.Order)
                .WithOne()
                .HasForeignKey<Tracking>(r => r.OrderID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Payment>()
                .HasOne(r => r.Order)
                .WithOne()
                .HasForeignKey<Payment>(r => r.OrderID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Session>()
                .HasOne(s => s.User) // Отношение "один ко многим": один пользователь может иметь много сессий
                .WithOne() // Обратная навигационная ссылка от Session к User
                .HasForeignKey<Session>(s => s.UserId);

            modelBuilder.Entity<Vehicle>()
                .HasOne(v => v.User)
                .WithOne()
                .HasForeignKey<Vehicle>(u => u.UserID);
            modelBuilder.Entity<Vehicle>()
                .HasOne(v => v.VehicleType)
                .WithOne()
                .HasForeignKey<Vehicle>(v => v.VehicleTypeID);

            // Настройка внешних ключей и связей между таблицами
            //modelBuilder.Entity<Cargo>()
            //    .HasOne(c => c.CargoType)
            //    .WithMany()
            //    .HasForeignKey(c => c.CargoTypeID);

            //modelBuilder.Entity<Cargo>()
            //    .HasOne(c => c.Owner)
            //    .WithMany()
            //    .HasForeignKey(c => c.UserID);

            //modelBuilder.Entity<Vehicle>()
            //    .HasOne(v => v.VehicleType)
            //    .WithMany()
            //    .HasForeignKey(v => v.VehicleTypeID);

            //modelBuilder.Entity<Order>()
            //    .HasOne(o => o.Cargo)
            //    .WithMany()
            //    .HasForeignKey(o => o.CargoID)
            //    .OnDelete(DeleteBehavior.Restrict); 
            //modelBuilder.Entity<Order>()
            //    .HasOne(o => o.Driver)
            //    .WithMany()
            //    .HasForeignKey(o => o.UserID);
            //modelBuilder.Entity<Order>()
            //    .HasOne(o => o.Status)
            //    .WithMany()
            //    .HasForeignKey(o => o.StatusID);

            //modelBuilder.Entity<Payment>()
            //    .HasOne(p => p.Order)
            //    .WithMany()
            //    .HasForeignKey(p => p.OrderID);

            //modelBuilder.Entity<Review>()
            //    .HasOne(r => r.Order)
            //    .WithMany()
            //    .HasForeignKey(r => r.OrderID)
            //    .OnDelete(DeleteBehavior.Restrict);
            //modelBuilder.Entity<Review>()
            //    .HasOne(r => r.Customer)
            //    .WithMany()
            //    .HasForeignKey(r => r.UserID);
            //modelBuilder.Entity<Session>()
            //    .HasOne(s => s.User)
            //    .WithMany()
            //    .HasForeignKey(s => s.UserId);
            //modelBuilder.Entity<Tracking>()
            //    .HasOne(t => t.Status)
            //    .WithMany()
            //    .HasForeignKey(t => t.StatusID);
            //modelBuilder.Entity<Tracking>()
            //    .HasOne(t => t.Order)
            //    .WithMany

            // Здесь можно добавить дополнительные настройки моделей
        }
    }

    public class Client
    {
        [Key]
        public string UserId { get; set; }

        // Дополнительные свойства для зрителей могут быть добавлены здесь

        [ForeignKey("UserId")]
        public User Users { get; set; }
    }

    public class Administrator
    {
        [Key]
        public string UserId { get; set; }

        // Дополнительные свойства для курьеров могут быть добавлены здесь

        [ForeignKey("UserId")]
        public User Users { get; set; }
    }

    public class Manager
    {
        [Key]
        public string UserId { get; set; }

        // Дополнительные свойства для менеджеров могут быть добавлены здесь

        [ForeignKey("UserId")]
        public User Users { get; set; }
    }
}
