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
            modelBuilder.Entity<User>().HasKey(u => u.Id);
            modelBuilder.Entity<Revenue>().HasKey(r => r.Id);
            modelBuilder.Entity<Expense>().HasKey(e => e.Id);
            modelBuilder.Entity<MetaFinancial>().HasKey(m => m.Id);
        }
        
    }
}


