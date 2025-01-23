using UserPanel.Shared.Models;

namespace UserPanel.Shared.Interfaces;

public interface ITicketRepository
{
    Task<List<Ticket>> GetAllTicketsAsync();
    Task<Ticket?> GetTicketByIdAsync(int id);
    Task CreateTicketAsync(Ticket ticket);
    Task UpdateTicketAsync(Ticket ticket);
    Task DeleteTicketAsync(int id);
}