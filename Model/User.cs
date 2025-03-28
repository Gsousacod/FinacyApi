
using System.ComponentModel.DataAnnotations;

namespace FinacyApi.Model
{
    public class User
    {
         [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public string? Role { get; set; }

        public decimal SalaryMonthly { get; set; }

        public DateTime DataCreated { get; set; } = DateTime.UtcNow;
       

         // Relacionamentos
        public List<Revenue> Revenues { get; set; } = new();  
        public List<Expense> Expenses { get; set; } = new();  
        public List<MetaFinancial> MetaFinancials { get; set; } = new();  
    }
}