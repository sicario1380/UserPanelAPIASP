using UserPanel.Shared.Models;

namespace UserPanel.Shared.Interfaces
{
    public interface IFollowUpChatRepository
    {
        Task<List<FollowUpChat>> GetChatsByTicketIdAsync(int ticketId);
        Task CreateChatAsync(FollowUpChat chat); // This method needs to be implemented OH YEAH
    }
}
