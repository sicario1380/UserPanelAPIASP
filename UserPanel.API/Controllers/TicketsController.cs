using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using UserPanel.Shared.DTOs;
using UserPanel.Shared.Interfaces;
using UserPanel.Shared.Models;

namespace UserPanel.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class TicketsController : ControllerBase
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<TicketsController> _logger;

        public TicketsController(ITicketRepository ticketRepository, IMapper mapper, ILogger<TicketsController> logger)
        {
            _ticketRepository = ticketRepository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<TicketDto>>> GetTickets()
        {
            try
            {
                var tickets = await _ticketRepository.GetAllTicketsAsync();
                var ticketDtos = _mapper.Map<List<TicketDto>>(tickets);
                return Ok(ticketDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting tickets.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error.");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<TicketDto>> GetTicket(int id)
        {
            try
            {
                var ticket = await _ticketRepository.GetTicketByIdAsync(id);
                if (ticket == null)
                {
                    return NotFound();
                }
                var ticketDto = _mapper.Map<TicketDto>(ticket);
                return Ok(ticketDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting ticket with ID {id}.", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error.");
            }
        }

        [HttpPost]
        [Authorize(Policy = "UserPolicy")]
        public async Task<ActionResult<TicketDto>> CreateTicket([FromBody] TicketDto ticketDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var ticket = _mapper.Map<Ticket>(ticketDto);
                ticket.DateIssued = DateTime.UtcNow;
                ticket.LastUpdate = DateTime.UtcNow;
                await _ticketRepository.CreateTicketAsync(ticket);

                var createdTicketDto = _mapper.Map<TicketDto>(ticket);
                return CreatedAtAction(nameof(GetTicket), new { id = ticket.TicketID }, createdTicketDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating ticket.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error.");
            }
        }

        [HttpPut("{id:int}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> UpdateTicket(int id, [FromBody] TicketDto ticketDto)
        {
            if (id != ticketDto.TicketID || !ModelState.IsValid)
            {
                return BadRequest(); // Or return detailed validation errors
            }

            try
            {
                var existingTicket = await _ticketRepository.GetTicketByIdAsync(id);
                if (existingTicket == null)
                {
                    return NotFound();
                }

                _mapper.Map(ticketDto, existingTicket); // Update existing ticket
                existingTicket.LastUpdate = DateTime.UtcNow; // Update last update time
                await _ticketRepository.UpdateTicketAsync(existingTicket);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating ticket with ID {id}.", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error.");
            }
        }

        [HttpPut("{id:int}/terminate")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> TerminateTicket(int id)
        {
            try
            {
                var existingTicket = await _ticketRepository.GetTicketByIdAsync(id);
                if (existingTicket == null)
                {
                    return NotFound();
                }

                existingTicket.Status = "Closed";
                existingTicket.LastUpdate = DateTime.UtcNow;
                await _ticketRepository.UpdateTicketAsync(existingTicket);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error terminating ticket with ID {id}.", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error.");
            }
        }

        [HttpDelete("{id:int}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> DeleteTicket(int id)
        {
            try
            {
                var ticket = await _ticketRepository.GetTicketByIdAsync(id);
                if (ticket == null)
                {
                    return NotFound();
                }

                await _ticketRepository.DeleteTicketAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting ticket with ID {id}.", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error.");
            }
        }
    }
}
