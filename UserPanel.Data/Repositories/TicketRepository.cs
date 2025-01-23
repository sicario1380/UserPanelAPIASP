using Microsoft.EntityFrameworkCore;
using UserPanel.Shared.Interfaces;
using UserPanel.Shared.Models;

namespace UserPanel.Data.Repositories;

public class TicketRepository : ITicketRepository
{
    private readonly UserPanelDbContext _context;

    public TicketRepository(UserPanelDbContext context)
    {
        _context = context;
    }

    public async Task<List<Ticket>> GetAllTicketsAsync() => await _context.Tickets.ToListAsync();
    public async Task<Ticket?> GetTicketByIdAsync(int id) => await _context.Tickets.FindAsync(id);
    public async Task CreateTicketAsync(Ticket ticket)
    {
        _context.Tickets.Add(ticket);
        await _context.SaveChangesAsync();
    }
    public async Task UpdateTicketAsync(Ticket ticket)
    {
        _context.Entry(ticket).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }
    public async Task DeleteTicketAsync(int id)
    {
        var ticket = await _context.Tickets.FindAsync(id);
        if (ticket != null)
        {
            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();
        }
    }
}