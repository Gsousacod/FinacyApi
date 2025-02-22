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

        // Definição das tabelas
        public DbSet<User> Users { get; set; }
        public DbSet<Revenue> Revenues { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<MetaFinancial> MetaFinancials { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuração de chaves estrangeiras
            modelBuilder.Entity<Revenue>()
                .HasOne<User>()
                .WithMany(u => u.Revenues)
                .HasForeignKey(r => r.UserId);

            modelBuilder.Entity<Expense>()
                .HasOne<User>()
                .WithMany(u => u.Expenses)
                .HasForeignKey(d => d.UserId);

            modelBuilder.Entity<MetaFinancial>()
                .HasOne<User>()
                .WithMany(u => u.MetaFinancials)
                .HasForeignKey(m => m.UserId);
        }
        
    }
}


