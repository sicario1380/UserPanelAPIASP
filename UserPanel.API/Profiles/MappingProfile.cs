using AutoMapper;
using UserPanel.Shared.Models;
using UserPanel.Shared.DTOs;

namespace UserPanel.API.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Ticket, TicketDto>();
        CreateMap<TicketDto, Ticket>();
        CreateMap<FollowUpChat, FollowUpChatDto>();
        CreateMap<FollowUpChatDto, FollowUpChat>();
    }
}