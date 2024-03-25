using Application.DTOs.Auth;
using AutoMapper;
using Domain.Entities;

namespace Application.MappingProfile;

public class Mapping : Profile
{
    public Mapping()
    {
        CreateMap<UserEntity, UserResponseDto>().ReverseMap();
    }
}