using System.ComponentModel.DataAnnotations;

namespace UserPanel.Shared.DTOs;

public class TicketDto
{
    public int TicketID { get; set; }

    [Required]
    [MaxLength(100)]
    public string Subject { get; set; } = string.Empty;

    [Required]
    [MaxLength(20)]
    public string Status { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string TicketType { get; set; } = string.Empty;

    [Required]
    [MaxLength(500)]
    public string Description { get; set; } = string.Empty;

    public DateTime LastUpdate { get; set; }
}