using JustEatAPI.Services;
using Microsoft.OpenApi.Models;
using AutoMapper;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddHttpClient<IRestaurantService, RestaurantService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "JustEatAPI", Version = "v1" });
});

// Configure CORS (if needed for React frontend)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", builder =>
    {
        builder.WithOrigins("http://localhost:3000") // React frontend URL
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "JustEatAPI v1");
    });
}
//  https://localhost:7092/swagger

app.UseCors("AllowReactApp");
app.MapControllers();
app.Run();
