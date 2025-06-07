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

app.UseWebSockets();

app.Use(async (context, next) =>
{
    if (context.Request.Path == "/ws")
    {
        // Handle WebSocket requests data here
        if (context.WebSockets.IsWebSocketRequest)
        {
            using var webSocket = await context.WebSockets.AcceptWebSocketAsync();

            while (true)
            {
                if (webSocket.State != System.Net.WebSockets.WebSocketState.Open)
                {
                    break; // Exit the loop if the WebSocket is not open
                }
                
                var message = new ArraySegment<byte>(new byte[1024]);

                webSocket.SendAsync(message, System.Net.WebSockets.WebSocketMessageType.Text, true, CancellationToken.None).Wait();

                Thread.Sleep(1000); // Simulate some processing delay
            }
        }
        else
        {
            context.Response.StatusCode = 400; // Bad Request
            await context.Response.WriteAsync("WebSocket requests only.");
        }
    }
    else
    {
        await next(context);
    }

});

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

await app.RunAsync();
