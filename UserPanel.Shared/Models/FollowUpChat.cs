using System.ComponentModel.DataAnnotations;

namespace UserPanel.Shared.Models
{
    public class FollowUpChat
    {
        [Key]
        public int ChatID { get; set; }

        [Required]
        public int TicketID { get; set; }

        [Required]
        [MaxLength(500)]
        public string Message { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Sender { get; set; } = string.Empty;

        public DateTime DateSent { get; set; }

        public Ticket Ticket { get; set; }
    }
}