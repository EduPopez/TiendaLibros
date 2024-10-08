using Microsoft.EntityFrameworkCore;
using Serilog;
using TiendaLibros.API.Configurations;
using TiendaLibros.API.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// EF
var cadenaConexion = builder.Configuration.GetConnectionString("TiendaLibrosConnection");
builder.Services.AddDbContext<TiendaLibrosContext>(options => options.UseSqlServer(cadenaConexion));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Serilog
builder.Host.UseSerilog((ctx, lc) => 
    lc.WriteTo.Console().ReadFrom.Configuration(ctx.Configuration)
);

// Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", b => b.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

// AutoMapper
builder.Services.AddAutoMapper(typeof(MapperConfig));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("Allowall");

app.UseAuthorization();

app.MapControllers();

app.Run();
