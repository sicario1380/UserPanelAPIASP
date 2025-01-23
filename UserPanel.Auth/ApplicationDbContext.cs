using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UserPanel.Shared.Models;

namespace UserPanel.Auth
{
public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ApplicationUser>(entity =>
        {
            entity.Property(u => u.FirstName).HasMaxLength(50).IsRequired();
            entity.Property(u => u.LastName).HasMaxLength(50).IsRequired();
            entity.Property(u => u.DateOfBirth).IsRequired();
            entity.Property(u => u.PhoneNumber).HasMaxLength(15);
            entity.Property(u => u.Address).HasMaxLength(255);
            entity.Property(u => u.ProfilePictureUrl).HasMaxLength(2048);
            entity.Property(u => u.CodeMeli).HasMaxLength(20);
        });
    }
}
}