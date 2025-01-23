using System.ComponentModel.DataAnnotations;

namespace UserPanel.Auth.DTOs
{
    public class LoginDto
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        public required string Password { get; set; }

        public bool RememberMe { get; set; } = false;

        [Required]
        public string Username { get; set; } = string.Empty;
    }
}