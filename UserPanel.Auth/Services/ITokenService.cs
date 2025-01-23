using Microsoft.AspNetCore.Identity;
using UserPanel.Shared.Models;

namespace UserPanel.Auth.Services
{
    public interface ITokenService
    {
        string GenerateToken(ApplicationUser user);
    }
}