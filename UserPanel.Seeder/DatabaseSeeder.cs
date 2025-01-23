using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserPanel.Shared.Models;
using UserPanel.Auth; // Ensure the namespace for ApplicationDbContext
using UserPanel.Data; // Ensure the namespace for UserPanelDbContext

namespace UserPanel.Seeder
{
    public class DatabaseSeeder
    {
        private readonly ApplicationDbContext _identityContext;
        private readonly UserPanelDbContext _appContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DatabaseSeeder(ApplicationDbContext identityContext, UserPanelDbContext appContext, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _identityContext = identityContext;
            _appContext = appContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task SeedDataAsync()
        {
            await SeedIdentityDataAsync(); // Seed identity data

            if (await _appContext.Tickets.AnyAsync())
            {
                return; // Database has been seeded
            }

            var tickets = GenerateTickets(20);
            await _appContext.Tickets.AddRangeAsync(tickets);

            await _appContext.SaveChangesAsync(); // Await SaveChangesAsync

            await SeedFollowUpChatsAsync(tickets); // Seed related chats
        }

        private async Task SeedIdentityDataAsync()
        {
            // Create roles if they do not exist
            string[] roles = { "Admin", "User" };
            foreach (var role in roles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // Create default users if they do not exist
            var adminEmail = "admin@example.com";
            var userEmail = "user@example.com";

            if (await _userManager.FindByEmailAsync(adminEmail) == null)
            {
                var adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    FirstName = "Admin",
                    LastName = "User",
                    DateOfBirth = DateTime.Now.AddYears(-30), // Example date of birth
                    PhoneNumber = "123-456-7890"
                };
                await _userManager.CreateAsync(adminUser, "AdminPassword123!"); // Use a strong password
                await _userManager.AddToRoleAsync(adminUser, "Admin");
            }

            if (await _userManager.FindByEmailAsync(userEmail) == null)
            {
                var regularUser = new ApplicationUser
                {
                    UserName = userEmail,
                    Email = userEmail,
                    FirstName = "Regular",
                    LastName = "User",
                    DateOfBirth = DateTime.Now.AddYears(-25),
                    PhoneNumber = "098-765-4321"
                };
                await _userManager.CreateAsync(regularUser, "UserPassword123!"); // Use a strong password
                await _userManager.AddToRoleAsync(regularUser, "User");
            }
        }

        private List<Ticket> GenerateTickets(int count)
        {
            var tickets = new List<Ticket>();
            for (int i = 1; i <= count; i++)
            {
                tickets.Add(new Ticket
                {
                    TicketType = $"Type {i % 4 + 1}",
                    Subject = $"Subject {i}",
                    Description = $"Description for ticket {i}.",
                    Status = (i % 3 == 0) ? "Closed" : (i % 2 == 0) ? "In Progress" : "Open",
                    DateIssued = DateTime.Now.AddDays(-i),
                    LastUpdate = DateTime.Now
                });
            }
            return tickets;
        }

        private async Task SeedFollowUpChatsAsync(List<Ticket> tickets)
        {
            if (await _appContext.FollowUpChats.AnyAsync())
            {
                return;
            }

            var chats = new List<FollowUpChat>();
            foreach (var ticket in tickets)
            {
                chats.AddRange(GenerateChatsForTicket(ticket));
            }

            await _appContext.FollowUpChats.AddRangeAsync(chats);
            await _appContext.SaveChangesAsync();
        }

        private List<FollowUpChat> GenerateChatsForTicket(Ticket ticket)
        {
            var random = new Random();
            int numberOfChats = random.Next(1, 4);
            var chats = new List<FollowUpChat>();
            for (int i = 1; i <= numberOfChats; i++)
            {
                chats.Add(new FollowUpChat
                {
                    TicketID = ticket.TicketID,
                    Message = $"Chat message {i} for ticket {ticket.TicketID}.",
                    Sender = (i % 2 == 0) ? "User" : "Agent",
                    DateSent = ticket.DateIssued.AddHours(i * 2)
                });
            }
            return chats;
        }
    }
}
