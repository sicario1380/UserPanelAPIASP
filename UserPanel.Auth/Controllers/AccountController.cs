using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UserPanel.Auth.DTOs;
using UserPanel.Shared.Models;
using UserPanel.Auth.Services;

namespace UserPanel.Auth.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenService _tokenService;
        public AccountController(UserManager<ApplicationUser> userManager, ITokenService tokenService, ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            logger.LogInformation("ITokenService resolved successfully.");
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var user = new ApplicationUser { UserName = registerDto.Username, Email = registerDto.Email };
            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (result.Succeeded)
            {
                return Ok();
            }
            return BadRequest(result.Errors);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, loginDto.Password))
            {
                var token = _tokenService.GenerateToken(user);
                return Ok(new { Token = token });
            }
            return Unauthorized();
        }
    }

    public interface ITokenService
    {
        string GenerateToken(ApplicationUser user);
    }
}
