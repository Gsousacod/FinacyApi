using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinacyApi.Model;
using Microsoft.EntityFrameworkCore;

namespace FinacyApi.Data
{
    public class FinancyDbContext : DbContext
    {

        public FinancyDbContext(DbContextOptions<FinancyDbContext> options) : base(options) { }


        public DbSet<User> Users { get; set; }
        public DbSet<Revenue> Revenues { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<MetaFinancial> MetaFinancials { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(u => u.Id);
            modelBuilder.Entity<Revenue>().HasKey(r => r.Id);
            modelBuilder.Entity<Expense>().HasKey(e => e.Id);
            modelBuilder.Entity<MetaFinancial>().HasKey(m => m.Id);


            modelBuilder.Entity<Revenue>()
                .HasOne(r => r.User)
                .WithMany(u => u.Revenues)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Expense>()
                .HasOne(e => e.User)
                .WithMany(u => u.Expenses)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<MetaFinancial>()
                .HasOne(m => m.User)
                .WithMany(u => u.MetaFinancials)
                .HasForeignKey(m => m.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}


