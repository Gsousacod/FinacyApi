
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace FinacyApi.Model
{
    public class EmergencyReservation
    {
         [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal value { get; set; }

        public DateTime LastUpdateDate{ get; set; } = DateTime.UtcNow;
    }
}