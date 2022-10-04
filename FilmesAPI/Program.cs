using FilmesAPI.Data;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

//Getting the MySQL connection string from appsettings.json.
string mySQLConnectionString = builder.Configuration.GetConnectionString("FilmeConnection");

var serverVersion = new MySqlServerVersion(new Version(8, 0, 30));

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Connecting with MySQL database.
builder.Services.AddDbContext<FilmesContext>(opts => opts.UseMySql(mySQLConnectionString, serverVersion));

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
