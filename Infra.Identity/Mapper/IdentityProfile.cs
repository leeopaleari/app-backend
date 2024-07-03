using Application.DTOs.User;
using AutoMapper;
using Infra.Data.Identity;

namespace Infra.Identity.Mapper;

public class IdentityProfile : Profile
{
    public IdentityProfile()
    {
        CreateMap<ApplicationUser, UserCreateRequest>()
            .ReverseMap();
    }
}