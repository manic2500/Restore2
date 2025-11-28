using System.Data.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Restore.API.Filters;
using Restore.API.Middlewares;
using Restore.Application;
using Restore.Infrastructure;
using Restore.Infrastructure.Persistence.DbContexts;
using Restore.Infrastructure.Persistence.Seeders;
/* using Restore.Infrastructure.Persistence.DbContexts;
using Restore.Infrastructure.Persistence.Seeders; */


var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

// Add services to the container.
services.AddScoped<ValidationExceptionFilter>();

services.AddControllers(options =>
{
    options.Filters.Add<ValidationExceptionFilter>();
});
services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});
services.AddScoped<DbConnection>(sp =>
{
    var config = sp.GetRequiredService<IConfiguration>();
    var connection = new NpgsqlConnection(config.GetConnectionString("DefaultConnection"));
    connection.Open();
    return connection;
});


services.AddInfrastructureServices();
services.AddApplicationServices();

services.AddCors();

var app = builder.Build();
app.UseMiddleware<GlobalExceptionMiddleware>();// ✅ Global exception middleware


app.UseCors(opt => opt.AllowAnyHeader().AllowAnyMethod().AllowCredentials().WithOrigins("https://localhost:3000"));
app.MapControllers();

// Seeding Data
try
{
    using var scope = app.Services.CreateScope();

    var context = scope.ServiceProvider.GetRequiredService<StoreDbContext>()
    ?? throw new InvalidOperationException("Failed to retrieve store context");

    DbInitializer.SeedData(context);
    DbInitializer.SeedTaxDelivery(context);
}
catch (Exception ex)
{
    Console.WriteLine(ex);
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


app.Run();





/* 
var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");
record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
 */


/* 

// Required for BasketContext
services.AddHttpContextAccessor();

app.MapWhen(
   context => context.Request.Path.StartsWithSegments("/api/basket"),
   basketApp =>
   {
       basketApp.UseMiddleware<BasketIdMiddleware>();

       basketApp.UseRouting();
       basketApp.UseEndpoints(endpoints =>
       {
           endpoints.MapControllers();
       });
   }
);


 */