
namespace FinacyApi.ViewModels
{
    public class RevenueViewModel
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; } // Relacionamento com usuário
    }
}