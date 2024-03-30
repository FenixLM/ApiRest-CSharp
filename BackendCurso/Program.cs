using BackendCurso.Automappers;
using BackendCurso.DTOs;
using BackendCurso.Models;
using BackendCurso.Repository;
using BackendCurso.Services;
using BackendCurso.Validator;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Add services to the container. Entity Framework Core
builder.Services.AddDbContext<StoreContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("StoreConnection")); 
});

// Add Validators
// Se registra el validador de BeerInsertDto
// IValidator es una interfaz de FluentValidation
builder.Services.AddScoped<IValidator<BeerInsertDto>, BeerInsertValidation >();

// Repositories 
builder.Services.AddScoped<IRepository<Beer>, BeerRepository>();

//Mapping
// Se registra el mapeo de las clases
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Services 

// Se registra el servicio de BeerService
//builder.Services.AddScoped<IBeerService, BeerService>();

// Se registra el servicio de BeerService con la interfaz ICommonService
builder.Services.AddKeyedScoped<ICommonService<BeerDto, BeerInsertDto, BeerUpdateDto>, BeerService>("beerService");

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
