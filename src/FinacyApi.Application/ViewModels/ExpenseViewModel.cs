using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinacyApi.ViewModels
{
    public class ExpenseViewModel
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; } // Relacionamento com usuário
        public string Category { get; set; }
    }
}