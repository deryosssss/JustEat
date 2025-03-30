using JustEatAPI.Controllers;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();  // Add this line to support controllers
builder.Services.AddOpenApi();
builder.Services.AddHttpClient();

// Configure CORS (if needed for React frontend)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", builder =>
    {
        builder.WithOrigins("http://localhost:3000")  // React frontend URL
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// Apply CORS policy
app.UseCors("AllowReactApp");  // Make sure this is called after UseHttpsRedirection()

// Map controllers to handle routes
app.MapControllers(); // This will map your restaurant controller to the API

app.Run();
