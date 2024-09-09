using AutoMapper;
using Real_Time_Chat_Application.Models.DTOs;
using System;

namespace Real_Time_Chat_Application.Models
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<User, UserDTO>();
            CreateMap<CreateUserDTO, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());
            CreateMap<UpdateUserDTO, User>();
            CreateMap<ChatMessage, ChatMessageDTO>();
            CreateMap<CreateChatMessageDTO, ChatMessage>();
            CreateMap<UpdateChatMessageDTO, ChatMessage>();
        }
    }
}
