using HotelReservationAPI.Infrastructure.Data;
using HotelReservationAPI.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using HotelReservationAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var mongoDbSettings = builder.Configuration.GetSection("MongoDB");

// Crear un cliente de MongoDB
builder.Services.AddSingleton<IMongoClient>(serviceProvider =>
{
    var connectionString = mongoDbSettings.GetValue<string>("ConnectionString");
    return new MongoClient(connectionString); // Configura el cliente MongoDB
});

// Crear el cliente para acceder a la base de datos
builder.Services.AddScoped<IMongoDatabase>(serviceProvider =>
{
    var client = serviceProvider.GetRequiredService<IMongoClient>();
    var databaseName = mongoDbSettings.GetValue<string>("DatabaseName");
    return client.GetDatabase(databaseName); // Accede a la base de datos en MongoDB
});

//Agragar sericio email 
builder.Services.AddScoped<EmailService>();

// Registrar el repositorio como servicio
builder.Services.AddScoped<IHotelRepository, HotelRepository>();
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Hotel Reservation API", Version = "v1" });
});

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
