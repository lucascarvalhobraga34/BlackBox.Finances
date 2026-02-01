using Application.Interfaces;
using Application.UseCases.ImportarFatura;
using FluentValidation;
using Infrastructure.Parsers;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Persistence.UnitOfWork;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlServer("Server=localhost;Database=BlackBox;Trusted_Connection=True;TrustServerCertificate=True;"));

builder.Services.AddScoped<ILancamentoFaturaRepository, LancamentoFaturaRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<ICartaoRepository, CartaoRepository>();
builder.Services.AddScoped<IFaturaRepository, FaturaRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IFaturaParserFactory, FaturaParserFactory>();
builder.Services.AddScoped<ImportarFaturaHandler>();

builder.Services.AddValidatorsFromAssembly(typeof(ImportarFaturaValidator).Assembly);

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(
        typeof(ImportarFaturaCommand).Assembly);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
