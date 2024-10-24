using AutoBid.WebApi.Data;
using Microsoft.EntityFrameworkCore;
using WebApi.Data.Models;
using WebApi.Interfaces.Mappers;
using WebApi.Interfaces.Models;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IMapper<CarOfferModel, CarOffer>, CarOfferMapper>();
builder.Services.AddTransient<IMapper<UserModel, User>, UserMapper>();
builder.Services.AddTransient(typeof(ICollectionMapper<,>), typeof(CollectionMapper<,>));
builder.Services.AddEntityFrameworkNpgsql().AddDbContext<AutoBidDbContext>(options => options.UseNpgsql(connectionString));
builder.Services.AddTransient<AutoBidContext>();

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
