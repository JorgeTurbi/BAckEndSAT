using AutoMapper;
using DTOs;
using Entities;

namespace SecctionProfile;

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

        // Session <-> SessionDto
        CreateMap<Session, SessionDto>().ReverseMap();

        // User -> UserProfileDto (mapeo personalizado)
        CreateMap<User, UserProfileDto>()
            .ForMember(
                dest => dest.InstitucionNombre,
                opt => opt.MapFrom(src => src.Institucion!.Nombre)
            )
            .ForMember(
                dest => dest.InstitucionCodigo,
                opt => opt.MapFrom(src => src.Institucion!.CodigoNombre)
            )
            .ForMember(
                dest => dest.DepartamentoDireccion,
                opt => opt.MapFrom(src => src.Departamento!.Direccion)
            )
            .ForMember(
                dest => dest.DepartamentoDirector,
                opt => opt.MapFrom(src => src.Departamento!.Director)
            );
    }
}
