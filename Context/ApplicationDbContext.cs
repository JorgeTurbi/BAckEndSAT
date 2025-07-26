using Entities;
using Microsoft.EntityFrameworkCore;

namespace Context;

public class ApplicationDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Institucion> Instituciones { get; set; }
    public DbSet<Departamento> Departamentos { get; set; }
    public DbSet<Session> Sessions { get; set; }
    public DbSet<Vacante> Vacantes { get; set; }
    public DbSet<CategoriaVacante> CategoriasVacante { get; set; }
    public DbSet<Provincia> Provincias { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder
            .Entity<User>()
            .HasOne(u => u.Institucion)
            .WithMany(i => i.Users)
            .HasForeignKey(u => u.InstitutionId);

        modelBuilder
            .Entity<Institucion>()
            .HasMany(i => i.Vacantes)
            .WithOne(v => v.Institucion!)
            .HasForeignKey(v => v.InstitucionId);

        modelBuilder
            .Entity<User>()
            .HasOne(u => u.Departamento)
            .WithMany(d => d.Users)
            .HasForeignKey(u => u.DepartamentoId);

        // Relación User -> Sessions
        modelBuilder
            .Entity<Session>()
            .HasOne(s => s.User)
            .WithMany(u => u.Sessions)
            .HasForeignKey(s => s.UserId);

        // Índices para mejorar rendimiento
        modelBuilder.Entity<Session>(e =>
        {
            e.Property(s => s.Token).HasColumnType("varchar(max)").IsRequired();

            e.HasIndex(s => new { s.UserId, s.IsRevoked });
        });

        modelBuilder.Entity<Vacante>(e =>
        {
            e.Property(v => v.DescripcionPuesto).HasColumnType("nvarchar(max)");
            e.Property(v => v.ResponsabilidadesEspecificas).HasColumnType("nvarchar(max)");
            e.Property(v => v.RequisitosGenerales).HasColumnType("nvarchar(max)");
            e.Property(v => v.EducacionRequerida).HasColumnType("nvarchar(max)");
            e.Property(v => v.ExperienciaRequerida).HasColumnType("nvarchar(max)");
            e.Property(v => v.HabilidadesCompetencias).HasColumnType("nvarchar(max)");
            e.Property(v => v.BeneficiosCompensaciones).HasColumnType("nvarchar(max)");

            e.HasOne(v => v.Institucion)
                .WithMany(i => i.Vacantes) // Agrega la colección en Institucion (ver abajo)
                .HasForeignKey(v => v.InstitucionId);

            e.HasOne(v => v.Provincia).WithMany(p => p.Vacantes).HasForeignKey(v => v.ProvinciaId);

            e.HasOne(v => v.Categoria).WithMany(c => c.Vacantes).HasForeignKey(v => v.CategoriaId);

            e.HasIndex(v => new
            {
                v.InstitucionId,
                v.CategoriaId,
                v.ProvinciaId,
            });
            e.HasIndex(v => v.IsActive);
        });

        // Datos por defecto para Departamentos
        modelBuilder
            .Entity<Departamento>()
            .HasData(
                new Departamento
                {
                    Id = 1,
                    Direccion = "Dirección de Tecnología",
                    Director = "Capitan de Navío Jhonathan de la Cruz",
                },
                new Departamento
                {
                    Id = 2,
                    Direccion = "Recursos Humanos",
                    Director = "Mayor Piloto Jesica Heredia",
                },
                new Departamento
                {
                    Id = 3,
                    Direccion = "Operaciones",
                    Director = "Coronel de Infantería Juan Pérez",
                }
            );
        //INstitucioes por defecto
        modelBuilder
            .Entity<Institucion>()
            .HasData(
                new Institucion
                {
                    Id = 1,
                    CodigoNombre = "CESAC",
                    Nombre = "Cuerpo Especializado en Seguridad Aeroportuaria y Aviación Civil",
                    UrlLogo = "/Logos/cesac.png",
                    Telefono = "809-999-1001",
                    Email = "info@cesac.mil.do",
                    Direccion = "Av. Aeropuerto, Santo Domingo",
                },
                new Institucion
                {
                    Id = 2,
                    CodigoNombre = "CECCOMM",
                    Nombre = "Cuerpo Especializado de Control de Combustibles",
                    UrlLogo = "/Logos/ceccomm.png",
                    Telefono = "809-999-1002",
                    Email = "info@ceccomm.mil.do",
                    Direccion = "Av. John F. Kennedy, Santo Domingo",
                },
                new Institucion
                {
                    Id = 3,
                    CodigoNombre = "CIUTRAN",
                    Nombre = "Fuerza de Tarea Conjunta Ciudad Tranquila",
                    UrlLogo = "/Logos/ciutran.png",
                    Telefono = "809-999-1003",
                    Email = "info@ciutran.mil.do",
                    Direccion = "Zona Militar, Santo Domingo",
                },
                new Institucion
                {
                    Id = 4,
                    CodigoNombre = "UNADE",
                    Nombre = "Universidad Nacional para la Defensa",
                    UrlLogo = "/Logos/unade.png",
                    Telefono = "809-999-1004",
                    Email = "info@unade.mil.do",
                    Direccion = "Ciudad Militar, Santo Domingo",
                },
                new Institucion
                {
                    Id = 5,
                    CodigoNombre = "ISSFFAA",
                    Nombre = "Instituto de Seguridad Social de las Fuerzas Armadas",
                    UrlLogo = "/Logos/issffaa.png",
                    Telefono = "809-999-1005",
                    Email = "info@issffaa.mil.do",
                    Direccion = "Calle Principal #10, Santo Domingo",
                },
                new Institucion
                {
                    Id = 6,
                    CodigoNombre = "HIFA",
                    Nombre = "Hospital de las Fuerzas Armadas",
                    UrlLogo = "/Logos/hifa.png",
                    Telefono = "809-999-1006",
                    Email = "info@hifa.mil.do",
                    Direccion = "Av. Independencia, Santo Domingo",
                },
                new Institucion
                {
                    Id = 7,
                    CodigoNombre = "EGDDHHyDIH",
                    Nombre =
                        "Escuela de Graduados de Derechos Humanos y Derecho Internacional Humanitario",
                    UrlLogo = "/Logos/egddhh.png",
                    Telefono = "809-999-1007",
                    Email = "info@egddhh.mil.do",
                    Direccion = "Ciudad Militar, Santo Domingo",
                },
                new Institucion
                {
                    Id = 8,
                    CodigoNombre = "FARD",
                    Nombre = "Fuerza Aérea de la República Dominicana",
                    UrlLogo = "/Logos/fard.png",
                    Telefono = "809-999-1008",
                    Email = "info@fard.mil.do",
                    Direccion = "Base Aérea San Isidro, Santo Domingo Este",
                },
                new Institucion
                {
                    Id = 9,
                    CodigoNombre = "ERD",
                    Nombre = "Ejército de la República Dominicana",
                    UrlLogo = "/Logos/ejercito.png",
                    Telefono = "809-999-1009",
                    Email = "info@ejercito.mil.do",
                    Direccion = "Av. 27 de Febrero, Santo Domingo",
                },
                new Institucion
                {
                    Id = 10,
                    CodigoNombre = "ARD",
                    Nombre = "Armada de la República Dominicana",
                    UrlLogo = "/Logos/armada.png",
                    Telefono = "809-999-1010",
                    Email = "info@armada.mil.do",
                    Direccion = "Base Naval, Santo Domingo",
                },
                new Institucion
                {
                    Id = 11,
                    CodigoNombre = "MIDE",
                    Nombre = "Ministerio de Defensa",
                    UrlLogo = "/Logos/mide.png",
                    Telefono = "809-999-1011",
                    Email = "info@mide.mil.do",
                    Direccion = "Av. Luperón, Santo Domingo",
                },
                new Institucion
                {
                    Id = 12,
                    CodigoNombre = "C5i",
                    Nombre = "C5i de las Fuerzas Armadas",
                    UrlLogo = "/Logos/mide.png",
                    Telefono = "809-999-1011",
                    Email = "info@C5iffaa.gob.do",
                    Direccion = "Av. Luperón, Santo Domingo",
                }
            );

        modelBuilder
            .Entity<Provincia>()
            .HasData(
                new Provincia { Id = 1, Nombre = "Azua" },
                new Provincia { Id = 2, Nombre = "Baoruco" },
                new Provincia { Id = 3, Nombre = "Barahona" },
                new Provincia { Id = 4, Nombre = "Dajabón" },
                new Provincia { Id = 5, Nombre = "Duarte" },
                new Provincia { Id = 6, Nombre = "Elías Piña" },
                new Provincia { Id = 7, Nombre = "El Seibo" },
                new Provincia { Id = 8, Nombre = "Espaillat" },
                new Provincia { Id = 9, Nombre = "Hato Mayor" },
                new Provincia { Id = 10, Nombre = "Hermanas Mirabal" },
                new Provincia { Id = 11, Nombre = "Independencia" },
                new Provincia { Id = 12, Nombre = "La Altagracia" },
                new Provincia { Id = 13, Nombre = "La Romana" },
                new Provincia { Id = 14, Nombre = "La Vega" },
                new Provincia { Id = 15, Nombre = "María Trinidad Sánchez" },
                new Provincia { Id = 16, Nombre = "Monseñor Nouel" },
                new Provincia { Id = 17, Nombre = "Monte Cristi" },
                new Provincia { Id = 18, Nombre = "Monte Plata" },
                new Provincia { Id = 19, Nombre = "Pedernales" },
                new Provincia { Id = 20, Nombre = "Peravia" },
                new Provincia { Id = 21, Nombre = "Puerto Plata" },
                new Provincia { Id = 22, Nombre = "Samaná" },
                new Provincia { Id = 23, Nombre = "Sánchez Ramírez" },
                new Provincia { Id = 24, Nombre = "San Cristóbal" },
                new Provincia { Id = 25, Nombre = "San José de Ocoa" },
                new Provincia { Id = 26, Nombre = "San Juan" },
                new Provincia { Id = 27, Nombre = "San Pedro de Macorís" },
                new Provincia { Id = 28, Nombre = "Santiago" },
                new Provincia { Id = 29, Nombre = "Santiago Rodríguez" },
                new Provincia { Id = 30, Nombre = "Santo Domingo" },
                new Provincia { Id = 31, Nombre = "Valverde" },
                new Provincia { Id = 32, Nombre = "Distrito Nacional" }
            );

        modelBuilder
            .Entity<CategoriaVacante>()
            .HasData(
                new CategoriaVacante { Id = 1, Nombre = "Operaciones Especiales" },
                new CategoriaVacante { Id = 2, Nombre = "Inteligencia y Contrainteligencia" },
                new CategoriaVacante { Id = 3, Nombre = "Seguridad Fronteriza" },
                new CategoriaVacante { Id = 4, Nombre = "Ciberseguridad" },
                new CategoriaVacante { Id = 5, Nombre = "Administración" },
                new CategoriaVacante { Id = 6, Nombre = "Logística" },
                new CategoriaVacante { Id = 7, Nombre = "Comunicaciones" }
            );
    }
}
