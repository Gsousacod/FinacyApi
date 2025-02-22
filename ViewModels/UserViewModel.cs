using System;
using System.Collections.Generic;

namespace FinacyApi.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public decimal SalaryMonthly { get; set; }

        public DateTime DataCreated { get; set; }

        public Dictionary<string, decimal> FinancialDistribution { get; set; } = new();

        
       
    }
}
