using Microsoft.EntityFrameworkCore;
using UserPanel.Shared.Models;

namespace UserPanel.Data;

public class UserPanelDbContext : DbContext
{
    public UserPanelDbContext(DbContextOptions<UserPanelDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(t => t.TicketID);
            entity.Property(t => t.TicketType).IsRequired().HasMaxLength(50);
            entity.Property(t => t.Subject).IsRequired().HasMaxLength(100);
            entity.Property(t => t.Description).IsRequired().HasMaxLength(500);
            entity.Property(t => t.Status).IsRequired().HasMaxLength(20);
            entity.Property(t => t.DateIssued).IsRequired();
            entity.Property(t => t.LastUpdate).IsRequired();
        });

        modelBuilder.Entity<FollowUpChat>(entity =>
        {
            entity.HasKey(c => c.ChatID);
            entity.Property(c => c.TicketID).IsRequired();
            entity.Property(c => c.Message).IsRequired().HasMaxLength(500);
            entity.Property(c => c.Sender).IsRequired().HasMaxLength(50);
            entity.Property(c => c.DateSent).IsRequired();

            entity.HasOne(c => c.Ticket)
                .WithMany(t => t.FollowUpChats)
                .HasForeignKey(c => c.TicketID)
                .OnDelete(DeleteBehavior.Cascade);
        });

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<FollowUpChat> FollowUpChats { get; set; }
}