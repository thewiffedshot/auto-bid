using AutoBid.WebApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using WebApi.Data.Models;
using WebApi.Interfaces.Mappers;
using WebApi.Interfaces.Models;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IMapper<CarOfferModel, CarOffer>, CarOfferMapper>();
builder.Services.AddTransient<IMapper<UserModel, User>, UserMapper>();
builder.Services.AddTransient(typeof(ICollectionMapper<,>), typeof(CollectionMapper<,>));
builder.Services.AddDbContext<AutoBidDbContext>(options => options.UseNpgsql(connectionString));

builder.Services.AddTransient<AutoBidContext>();

var app = builder.Build();

var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<AutoBidDbContext>();

var databaseConfigured = false;

while (!databaseConfigured)
{
    databaseConfigured = (context.Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator)?.Exists() ?? false;

    try
    {
        if (databaseConfigured)
        {
            try
            {
                context.Database.Migrate();
                Console.WriteLine(context.Database.GetAppliedMigrations().LastOrDefault());
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to migrate database. Retrying in 30 seconds.");
                Console.WriteLine(e.Message);
                databaseConfigured = false;
                await Task.Delay(30000);
            }
        }
    }
    catch (Exception e)
    {
        Console.WriteLine("Failed to connect to database. Retrying in 30 seconds.");
        Console.WriteLine(e.Message);
        databaseConfigured = false;   
        await Task.Delay(30000);
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
