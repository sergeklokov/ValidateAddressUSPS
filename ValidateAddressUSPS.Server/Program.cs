using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Add OpenAPI / Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseDefaultFiles();
app.MapStaticAssets();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Enable OpenAPI/Swagger UI in development
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

// code to figure out on which address is my API
//var addresses = app.Services.GetService(typeof(IServer)) is Microsoft.AspNetCore.Hosting.Server.IServer server
//    ? server.Features.Get<IServerAddressesFeature>()?.Addresses
//    : app.Urls;

//Console.WriteLine("Listening on: " + string.Join(", ", addresses ?? Array.Empty<string>()));

app.Run();
