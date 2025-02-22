
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
        public string Password { get; set; } // Senha será armazenada criptografada

        public decimal SalaryMonthly { get; set; }

        public DateTime DataCreated { get; set; } = DateTime.UtcNow;

         // Relacionamentos
        public List<Revenue> Revenues { get; set; } = new List<Revenue>();
        public List<Expense> Expenses { get; set; } = new List<Expense>();
        public List<MetaFinancial> MetaFinancials { get; set; } = new List<MetaFinancial>();
        public EmergencyReservation? EmergencyReservation { get; set; } // 1:1

        public Dictionary<string, decimal> CalculateFinancialDistribution
()
    {
        var distribution = new Dictionary<string, decimal>();

        if (SalaryMonthly <= 0)
        {
            return distribution;
        }

        // Definição das porcentagens (pode ser ajustável no futuro)
        decimal percentageExpenses = 0.50m;  
        decimal percentageReserve = 0.20m;   
        decimal percentageOthers = 0.30m;    

        distribution["FixedExpenses"] = SalaryMonthly * percentageExpenses; // 50% para despesas fixas
        distribution["Emergency Reserve"] = SalaryMonthly * percentageReserve; // 20% para reserva de emergência
        distribution["Outhers"] = SalaryMonthly * percentageOthers; // 30% para investimentos e lazer

        return distribution;
    }

    }
}