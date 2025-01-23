namespace UserPanel.Shared.DTOs;

public class FollowUpChatDto
{
    public int ChatID { get; set; }
    public int TicketID { get; set; }
    public string Message { get; set; } = string.Empty;
    public string Sender { get; set; } = string.Empty;
    public DateTime DateSent { get; set; }
}