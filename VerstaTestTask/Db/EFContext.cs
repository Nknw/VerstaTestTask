using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace VerstaTestTask.Db
{
    public class EFContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }

        public EFContext(DbContextOptions<EFContext> options) :base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .Property(o => o.SenderCity).HasColumnType("varchar(50)")
                .IsRequired();

            modelBuilder.Entity<Order>()
                .Property(o => o.RecipientCity).HasColumnType("varchar(50)")
                .IsRequired();

            modelBuilder.Entity<Order>()
                .Property(o => o.SenderAddress).HasColumnType("varchar(200)")
                .IsRequired();

            modelBuilder.Entity<Order>()
               .Property(o => o.RecipientAddress).HasColumnType("varchar(200)")
               .IsRequired();

            modelBuilder.Entity<Order>()
                .Property(o => o.Weight).HasColumnType("numeric(6,2)")
                .IsRequired();

            modelBuilder.Entity<Order>()
                .Property(o => o.ShipmentDate)
                .IsRequired();
        }

    }
}
