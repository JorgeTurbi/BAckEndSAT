using Entities;
using Microsoft.EntityFrameworkCore;

namespace Context;

public class ApplicationDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Institucion> Instituciones { get; set; }
    public DbSet<Departamento> Departamentos { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .HasOne(u => u.Institucion)
            .WithMany(i => i.Users)
            .HasForeignKey(u => u.InstitutionId);

        modelBuilder.Entity<User>()
            .HasOne(u => u.Departamento)
            .WithMany(d => d.Users)
            .HasForeignKey(u => u.DepartamentoId);

        modelBuilder.Entity<User>()
            .HasOne(u => u.Institucion)
            .WithMany(i => i.Users)
            .HasForeignKey(u => u.InstitutionId);

    // Datos por defecto para Departamentos
    modelBuilder.Entity<Departamento>().HasData(
        new Departamento
        {
            Id = 1,
            Direccion = "Dirección de Tecnología",
            Director = "Capitan de Navío Jhonathan de la Cruz"
        },
        new Departamento
        {
            Id = 2,
            Direccion = "Recursos Humanos",
            Director = "Mayor Piloto Jesica Heredia"
        },
           new Departamento
        {
            Id = 3,
            Direccion = "Operaciones",
            Director = "Coronel de Infantería Juan Pérez"
        }
    );
        //INstitucioes por defecto
        modelBuilder.Entity<Institucion>().HasData(
    new Institucion
    {
        Id = 1,
        CodigoNombre = "CESAC",
        Nombre = "Cuerpo Especializado en Seguridad Aeroportuaria y Aviación Civil",
        UrlLogo = "/Logos/cesac.png",
        Telefono = "809-999-1001",
        Email = "info@cesac.mil.do",
        Direccion = "Av. Aeropuerto, Santo Domingo"
    },
    new Institucion
    {
        Id = 2,
        CodigoNombre = "CECCOMM",
        Nombre = "Cuerpo Especializado de Control de Combustibles",
        UrlLogo = "/Logos/ceccomm.png",
        Telefono = "809-999-1002",
        Email = "info@ceccomm.mil.do",
        Direccion = "Av. John F. Kennedy, Santo Domingo"
    },
    new Institucion
    {
        Id = 3,
        CodigoNombre = "CIUTRAN",
        Nombre = "Fuerza de Tarea Conjunta Ciudad Tranquila",
        UrlLogo = "/Logos/ciutran.png",
        Telefono = "809-999-1003",
        Email = "info@ciutran.mil.do",
        Direccion = "Zona Militar, Santo Domingo"
    },
    new Institucion
    {
        Id = 4,
        CodigoNombre = "UNADE",
        Nombre = "Universidad Nacional para la Defensa",
        UrlLogo = "/Logos/unade.png",
        Telefono = "809-999-1004",
        Email = "info@unade.mil.do",
        Direccion = "Ciudad Militar, Santo Domingo"
    },
    new Institucion
    {
        Id = 5,
        CodigoNombre = "ISSFFAA",
        Nombre = "Instituto de Seguridad Social de las Fuerzas Armadas",
        UrlLogo = "/Logos/issffaa.png",
        Telefono = "809-999-1005",
        Email = "info@issffaa.mil.do",
        Direccion = "Calle Principal #10, Santo Domingo"
    },
    new Institucion
    {
        Id = 6,
        CodigoNombre = "HIFA",
        Nombre = "Hospital de las Fuerzas Armadas",
        UrlLogo = "/Logos/hifa.png",
        Telefono = "809-999-1006",
        Email = "info@hifa.mil.do",
        Direccion = "Av. Independencia, Santo Domingo"
    },
    new Institucion
    {
        Id = 7,
        CodigoNombre = "EGDDHHyDIH",
        Nombre = "Escuela de Graduados de Derechos Humanos y Derecho Internacional Humanitario",
        UrlLogo = "/Logos/egddhh.png",
        Telefono = "809-999-1007",
        Email = "info@egddhh.mil.do",
        Direccion = "Ciudad Militar, Santo Domingo"
    },
    new Institucion
    {
        Id = 8,
        CodigoNombre = "FARD",
        Nombre = "Fuerza Aérea de la República Dominicana",
        UrlLogo = "/Logos/fard.png",
        Telefono = "809-999-1008",
        Email = "info@fard.mil.do",
        Direccion = "Base Aérea San Isidro, Santo Domingo Este"
    },
    new Institucion
    {
        Id = 9,
        CodigoNombre = "ERD",
        Nombre = "Ejército de la República Dominicana",
        UrlLogo = "/Logos/ejercito.png",
        Telefono = "809-999-1009",
        Email = "info@ejercito.mil.do",
        Direccion = "Av. 27 de Febrero, Santo Domingo"
    },
    new Institucion
    {
        Id = 10,
        CodigoNombre = "ARD",
        Nombre = "Armada de la República Dominicana",
        UrlLogo = "/Logos/armada.png",
        Telefono = "809-999-1010",
        Email = "info@armada.mil.do",
        Direccion = "Base Naval, Santo Domingo"
    },
    new Institucion
    {
        Id = 11,
        CodigoNombre = "MIDE",
        Nombre = "Ministerio de Defensa",
        UrlLogo = "/Logos/mide.png",
        Telefono = "809-999-1011",
        Email = "info@mide.mil.do",
        Direccion = "Av. Luperón, Santo Domingo"
    },
        new Institucion
    {
        Id = 12,
        CodigoNombre = "C5i",
        Nombre = "C5i de las Fuerzas Armadas",
        UrlLogo = "/Logos/mide.png",
        Telefono = "809-999-1011",
        Email = "info@C5iffaa.gob.do",
        Direccion = "Av. Luperón, Santo Domingo"
    }
);

    }
}