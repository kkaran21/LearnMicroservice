using AutoMapper;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using PlatformService.Dtos;
using PlatformService.Models;

namespace PlatformService.Profiles
{
    public class PlatformsProfile : Profile
    {
        public PlatformsProfile()
        {
            CreateMap<Platform, PlatformReadDto>();
            CreateMap<PlatformCreateDto, Platform>();
            CreateMap<PlatformReadDto, PlatformPublishDto>();
            CreateMap<Platform, grpcPlatformModel>().
            ForMember(dest => dest.PlatformId ,opt => opt.MapFrom(src => src.Id));

        }

    }
}