using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserPanel.Shared.Interfaces;
using UserPanel.Shared.Models;

namespace UserPanel.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FollowUpChatsController : ControllerBase
    {
        private readonly IFollowUpChatRepository _chatRepository;
        private readonly ILogger<FollowUpChatsController> _logger;

        public FollowUpChatsController(IFollowUpChatRepository chatRepository, ILogger<FollowUpChatsController> logger)
        {
            _chatRepository = chatRepository;
            _logger = logger;
        }

        [HttpGet("{ticketId}")]
        public async Task<ActionResult<List<FollowUpChat>>> GetChatsByTicketId(int ticketId)
        {
            var chats = await _chatRepository.GetChatsByTicketIdAsync(ticketId);
            return Ok(chats);
        }

        [HttpPost("admin")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<ActionResult<FollowUpChat>> CreateAdminChat(FollowUpChat chat)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            chat.DateSent = DateTime.Now;
            chat.Sender = "Admin";
            await _chatRepository.CreateChatAsync(chat);
            return CreatedAtAction(nameof(GetChatsByTicketId), new { ticketId = chat.TicketID }, chat);
        }

        [HttpPost("user")]
        [Authorize(Policy = "UserPolicy")]
        public async Task<ActionResult<FollowUpChat>> CreateUserChat(FollowUpChat chat)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            chat.DateSent = DateTime.Now;
            chat.Sender = "User";
            await _chatRepository.CreateChatAsync(chat);
            return CreatedAtAction(nameof(GetChatsByTicketId), new { ticketId = chat.TicketID }, chat);
        }
    }
}
