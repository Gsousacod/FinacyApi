

namespace FinacyApi.ViewModels
{
    public class MetaFinacialViewModel
    {
        public int Id { get; set; }

        public int UserId { get; set; } // Relacionamento com usuário
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public int Year { get; set; }
        public decimal TotalValue { get; set; }
        private decimal _monthlyValue;

        public decimal MonthlyValue
        {
            get { return _monthlyValue == 0 ? TotalValue / 12 : _monthlyValue; }
            set { _monthlyValue = value; }
        }


    }
}