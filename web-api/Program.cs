using AutoBid.WebApi.Data;
using Microsoft.AspNetCore.Builder;
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

// Guids are 16 bytes long and decimal too, so we can receive the dictionary values as a byte arrays
var messageSize = 32; // 16 bytes for Guid + 16 bytes for decimal
var clientBids = new Dictionary<Guid, decimal>();

app.Map("/ws", async (context) =>
{
    // Handle WebSocket requests data here
    if (context.WebSockets.IsWebSocketRequest)
    {
        using var webSocket = await context.WebSockets.AcceptWebSocketAsync();

        var buffer = new byte[1024 * 4];
        var segment = new ArraySegment<byte>(buffer);

        while (webSocket.State == System.Net.WebSockets.WebSocketState.Open)
        {
            var result = await webSocket.ReceiveAsync(segment, CancellationToken.None);

            if (result.MessageType == System.Net.WebSockets.WebSocketMessageType.Close)
            {
                break; // Exit the loop if the WebSocket is not open
            }

            if (segment.Count < messageSize)
            {
                continue; // Skip if the received message is smaller than expected
            }

            var receivedString = System.Text.Encoding.UTF8.GetString(buffer, 0, result.Count);

            var offerGuid = receivedString.Substring(1, 37); // Assuming the Guid is at the start of the string

            // if (!Guid.TryParse(offerGuid, out var guid))
            // {
            //     Console.WriteLine("Invalid Guid received.");
            //     continue; // Skip processing if the Guid is invalid
            // }

            // Assuming the decimal value is after the Guid in the string
            var bid = receivedString.Substring(37); // Adjust based on your string format

            // if (!decimal.TryParse(bid, out var decimalBid))
            // {
            //     Console.WriteLine("Invalid bid amount received.");
            //     continue; // Skip processing if the bid is invalid
            // }

            Thread.Sleep(1000); // Simulate some processing delay

            Console.WriteLine($"Received bid for offer: {offerGuid} - {bid}");
        }
    }
    else
    {
        context.Response.StatusCode = 400; // Bad Request
        await context.Response.WriteAsync("WebSocket requests only.");
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
