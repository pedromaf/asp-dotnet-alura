using FilmesAPI.Data;
using FilmesAPI.Models.Entities;
using FilmesAPI.Services;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Resources;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

#region Service layer dependency injection.
builder.Services.AddScoped<MoviesService>();
builder.Services.AddScoped<MovieTheaterService>();
builder.Services.AddScoped<MovieSessionService>();
builder.Services.AddScoped<AddressService>();
builder.Services.AddScoped<ManagerService>();
#endregion

#region DB Configuration
string mySQLConnectionString = builder.Configuration.GetConnectionString("FilmeConnection");
var serverVersion = new MySqlServerVersion(new Version(8, 0, 30));

// Connecting with MySQL database.
builder.Services.AddDbContext<FilmesContext>(opts => opts.UseLazyLoadingProxies().UseMySql(mySQLConnectionString, serverVersion));
#endregion

// AutoMapper initialization.
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

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
