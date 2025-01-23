using Microsoft.EntityFrameworkCore;
using UserPanel.Shared.Interfaces;
using UserPanel.Shared.Models;

namespace UserPanel.Data.Repositories
{
    public class FollowUpChatRepository : IFollowUpChatRepository
    {
        private readonly UserPanelDbContext _context;

        public FollowUpChatRepository(UserPanelDbContext context)
        {
            _context = context;
        }

        public async Task<List<FollowUpChat>> GetAllFollowUpChatsAsync()
        {
            return await _context.FollowUpChats.ToListAsync();
        }

        public async Task<FollowUpChat> GetFollowUpChatByIdAsync(int id)
        {
            return await _context.FollowUpChats.FindAsync(id);
        }

        public async Task<List<FollowUpChat>> GetChatsByTicketIdAsync(int ticketId)
        {
            return await _context.FollowUpChats.Where(c => c.TicketID == ticketId).ToListAsync();
        }

        // Change this method name to match the interface
        public async Task CreateChatAsync(FollowUpChat chat)
        {
            _context.FollowUpChats.Add(chat);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateFollowUpChatAsync(FollowUpChat chat)
        {
            _context.FollowUpChats.Update(chat);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteFollowUpChatAsync(int id)
        {
            var chatToDelete = await _context.FollowUpChats.FindAsync(id);
            if (chatToDelete != null)
            {
                _context.FollowUpChats.Remove(chatToDelete);
                await _context.SaveChangesAsync();
            }
        }
    }
}
