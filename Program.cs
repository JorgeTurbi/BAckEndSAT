using System.Text;
using BackEndSAT.Middlewares;
using BackEndSAT.Services.IEstadistica;
using Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Repository;
using Repository.AplicacionesVacantes;
using Repository.Estadistica;
using Repository.InstitucionesRepository;
using Repository.InstitucionesRepository.PerfilesRepository;
using Repository.PerlesRepository;
using Repository.SessionServices;
using SecctionProfile;
using Services;
using Services.AplicacionesVacante;
using Services.Instituciones;
using Services.Perfil;
using Services.SessionServices;

var builder = WebApplication.CreateBuilder(args);
// Política de CORS abierta
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy
            .WithOrigins(
                "https://traslados.c5iffaa.gob.do", // Dominio Cloudflare
                "https://localhost:7114",           // Localhost dev
                "http://localhost:5021",
                "http://localhost:4200",
                "https://sat.c5iffaa.gob.do"
            )
             .AllowAnyHeader()
             .AllowAnyMethod();
    });
});

// builder.Services.AddHttpsRedirection(options =>
// {
//     options.HttpsPort = 443;
// });


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Leer la configuración del appsettings
var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]!);

builder
    .Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidateAudience = true,
            ValidAudience = jwtSettings["Audience"],
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,
        };
    });

// Add services to the container.
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ISessionValidationService, SessionValidationService>();

// Provincia
builder.Services.AddScoped<IProvinciaService, ProvinciaService>();
builder.Services.AddScoped<IProvinciaRepository, ProvinciaRepository>();

// CategoriaVacante
builder.Services.AddScoped<ICategoriaVacanteService, CategoriaVacanteService>();
builder.Services.AddScoped<ICategoriaVacanteRepository, CategoriaVacanteRepository>();

// Vacante
builder.Services.AddScoped<IVacanteService, VacanteService>();
builder.Services.AddScoped<IVacanteRepository, VacanteRepository>();
// Institucion
builder.Services.AddScoped<IInstitucion, InstitucionRepository>();
builder.Services.AddScoped<IInterfacePerfil, Generico>();
builder.Services.AddScoped<IUserProfileService, UserProfileService>();
builder.Services.AddScoped<IAplicacionVacanteService, AplicacionVacanteRepository>();
builder.Services.AddScoped<IEstadisticaService, EstadisticaRepository>();


builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "SAT API", Version = "v1" });

      // Servidores correctos
    c.AddServer(new OpenApiServer { Url = "https://traslados.c5iffaa.gob.do" });
    c.AddServer(new OpenApiServer { Url = "https://localhost:7114" });

    // Definición del esquema de seguridad (el candado)
    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Introduce el JWT con el esquema **Bearer**. Ej: `Bearer eyJhbGciOi...`",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" },
    };

    c.AddSecurityDefinition("Bearer", securityScheme);

    // Requisito global: todos los endpoints usarán este esquema (muestra el candado)
    c.AddSecurityRequirement(
        new OpenApiSecurityRequirement { { securityScheme, Array.Empty<string>() } }
    );
});

builder.Services.AddAutoMapper(typeof(MappingProfile)); // O typeof(MappingProfile)


var app = builder.Build();

// -------------------- Middleware --------------------
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedProto | ForwardedHeaders.XForwardedFor
});

app.UseHttpsRedirection();

// CORS primero
app.UseCors("AllowAll");

app.UseAuthentication();
app.UseMiddleware<SessionValidationMiddleware>();
app.UseAuthorization();

if (app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "SAT API v1");
    });
}

app.MapControllers();

app.Run();