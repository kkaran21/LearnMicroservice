using AutoMapper;
using CommandService.Dtos;
using CommandService.Models;

namespace CommandService.Profiles
{
    public class CommandsProfile : Profile
    {
        public CommandsProfile()
        {
            CreateMap<Command, CommandReadDto>();
            CreateMap<Platform, PlatformReadDto>();
            CreateMap<CommandCreateDto, Command>();
            CreateMap<PlatformPublishDto, Platform>()
            .ForMember(dest => dest.ExternalId, opt => opt.MapFrom(src => src.Id));
            // CreateMap<PlatformService.grpcPlatformModel,Platform>().
            // ForMember(dest => dest.ExternalId ,opt => opt.MapFrom(src => src.PlatformId))
            // .ForMember(dest => dest.Name ,opt => opt.MapFrom(src => src.Name))
            // .ForMember(dest => dest.Commands ,opt => opt.Ignore());
            
        }

    }
}