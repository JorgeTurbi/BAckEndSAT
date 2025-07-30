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

        CreateMap<Provincia, ProvinciaDto>().ReverseMap();

        CreateMap<CategoriaVacante, CategoriaVacanteDto>().ReverseMap();

        CreateMap<Vacante, VacanteDto>()
            .ForMember(d => d.InstitucionNombre, o => o.MapFrom(s => s.Institucion!.Nombre))
            .ForMember(d => d.ProvinciaNombre, o => o.MapFrom(s => s.Provincia!.Nombre))
            .ForMember(d => d.CategoriaNombre, o => o.MapFrom(s => s.Categoria!.Nombre));

        CreateMap<VacanteCreateDto, Vacante>().ReverseMap();
        CreateMap<VacanteUpdateDto, Vacante>().ReverseMap();
        CreateMap<InstitucionMilitarDTO, InstitucionMilitar>().ReverseMap();
        CreateMap<RangoDTO, Rango>().ReverseMap();


        // Mapeo de DTO a Entidad
        CreateMap<ApplicanteDto, Aplicante>().ReverseMap();
        CreateMap<ApplicanteDto, Aplicante>()
            .ForMember(dest => dest.Experience, opt => opt.MapFrom(src => src.Experience))
            .ForMember(dest => dest.Education, opt => opt.MapFrom(src => src.Education));

        CreateMap<ExperienceDto, Experience>()
            .ForMember(dest => dest.Id, opt => opt.Ignore()) // Ignora el Id para inserciones nuevas
            .ForMember(dest => dest.AplicanteId, opt => opt.Ignore()); // Se setea automáticamente

        CreateMap<EducationDto, Education>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.AplicanteId, opt => opt.Ignore());

        CreateMap<Experience, ExperienceDto>().ReverseMap();
        CreateMap<Education, EducationDto>().ReverseMap();
        CreateMap<AplicacionVacante, AplicacionVacanteDto>().ReverseMap();
 

    }
}
