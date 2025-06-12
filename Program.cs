using ConcesionariaVehiculos.Data;
using ConcesionariaVehiculos.DTOs;
using ConcesionariaVehiculos.Mapping;
using ConcesionariaVehiculos.Repositories;
using ConcesionariaVehiculos.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;



var builder = WebApplication.CreateBuilder(args);

// Configuración de servicios
builder.Services.AddControllers()
    .AddFluentValidation();
builder.Services.AddControllers()|
    .AddFluentValidation(fv =>
        fv.RegisterValidatorsFromAssemblyContaining<ClienteDTOValidator>());


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API Vehiculos XL – Concesionaria",
        Version = "v1",
        Description = "Venta y Distribución de Autos",
        Contact = new OpenApiContact
        {
            Name = "Sabrina Benitez",
            Email = "tecnoabry@gmail.com"
        }
    });


    // Configuración de Entity Framework Core
    builder.services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


    // Repositorios
    builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
    builder.Services.AddScoped<ClienteRepository>();
    builder.Services.AddScoped<IVehiculoRepository, VehiculoRepository>();
    builder.Services.AddScoped<VehiculoRepository>();
    builder.Services.AddScoped<IVentaRepository, VentaRepository>();
    builder.Services.AddScoped<VentaRepository>();
    builder.Services.AddScoped<IServicioPosVentaRepository, ServicioPosventaRepository>();
    builder.Services.AddScoped<ServicioPosventaRepository>();
    builder.Services.AddScoped<IFacturaRopository, FacturaRepository>();
    builder.Services.AddScoped<FacturaRepository>();

    // Servicios
    builder.Services.AddScoped<ClienteService>();
    builder.Services.AddScoped<VehiculoService>();
    builder.Services.AddScoped<VentaService>();
    builder.Services.AddScoped<ServicioPosVentaService>();

    // AutoMapper
    builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

    // Validaciones
    builder.Services.AddValidatorsFromAssemblyContaining<ClienteDTOValidator>();
    builder.Services.AddValidatorsFromAssemblyContaining<VehiculoDTOValidator>();
    builder.Services.AddValidatorsFromAssemblyContaining<VehiculoDTOValidator>();
    builder.Services.AddValidatorsFromAssemblyContaining<ServicioPosVentaDTOValidator>();
    builder.Services.AddValidatorsFromAssemblyContaining<FacturaDTOValidator>();

    // Configuración de CORS
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAll", policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
    });

    var app = builder.Build();

    // Configuración de middleware
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseCors("AllowAll");

    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();

    app.Run();

