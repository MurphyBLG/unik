using AutoMapper;
using BulletinBoard.Core.Models;
using BulletinBoard.Server.Contracts;

namespace BulletinBoard.Server.Profiles;

public class AdvertisementMapperProfile : Profile
{
    public AdvertisementMapperProfile()
    {
        CreateMap<CreateAdContract, Advertisement>()
            .ForMember(dest => dest.Message, src => src.MapFrom(s => s.Advertisement));
    }
}
