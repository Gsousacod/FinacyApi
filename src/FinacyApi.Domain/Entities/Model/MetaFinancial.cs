

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace FinacyApi.Model
{
    public class MetaFinancial
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; } // Chave estrangeira

        public string Description { get; set; }

        [ForeignKey("UserId")]
        public required User User { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalValue { get; set; }

        public DateTime Date { get; set; }

        [Column(TypeName = "decimal(18,2)")]
       private decimal _monthlyValue;

        public decimal MonthlyValue
        {
            get { return _monthlyValue == 0 ? TotalValue / 12 : _monthlyValue; }
            set { _monthlyValue = value; }
        }
    }
}