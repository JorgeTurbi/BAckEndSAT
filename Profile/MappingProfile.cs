
using AutoMapper;
using DTOs;
using Entities;


namespace MappingProfile;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // User <-> UserDto
        CreateMap<User, UserDto>().ReverseMap();

        // Institucion <-> InstitucionDto
        CreateMap<Institucion, InstitucionDto>().ReverseMap();

        // Departamento <-> DepartamentoDto
        CreateMap<Departamento, DepartamentoDto>().ReverseMap();
    }
}
