using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace UserPanel.Shared.Models
{
    public class Ticket
    {
        [Key]
        public int TicketID { get; set; }

        [Required]
        [MaxLength(50)]
        public string TicketType { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Subject { get; set; } = string.Empty;

        [Required]
        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public string Status { get; set; } = string.Empty;

        public DateTime DateIssued { get; set; }
        public DateTime LastUpdate { get; set; }

        public List<FollowUpChat> FollowUpChats { get; set; } = new List<FollowUpChat>();
    }
}