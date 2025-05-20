using Common.Config;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration.AddJsonFile("appsettings.json").Build();

builder.Services.AddSingleton<DatabaseSettings>((serviceProvider) =>
{
    var settings = new DatabaseSettings();
    configuration.GetSection("DatabaseSettings").Bind(settings);
    return settings;
});

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<DataSource.ApplicationDbContext>(
    (startupHelper, options) =>
    // options =>
    {
        options.UseNpgsql(
            startupHelper.GetRequiredService<DatabaseSettings>().GetConnectionString(),
            o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery));
            
            // builder.Configuration.GetConnectionString("DefaultConnection"),
            // o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery));
        //options.AddInterceptors(serviceProvider.GetRequiredService<ApplicationContextDbCommandInterceptor>());

        // var settings = builder.Configuration.GetSettings<AnalyticsSettings>();
        // if (settings.LogRawSql)
        // {
        //     options.LogTo(Log.Logger.Information, LogLevel.Information);
        // }
    });

/*.AddDbContext<ApplicationDbContext>(
    (serviceProvider, options) =>
    {
        options.UseNpgsql(
            serviceProvider.GetRequiredService<DatabaseSettings>().GetConnectionString(),
            o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery));
        options.AddInterceptors(serviceProvider.GetRequiredService<ApplicationContextDbCommandInterceptor>());

        var settings = Configuration.GetSettings<AnalyticsSettings>();
        if (settings.LogRawSql)
        {
            options.LogTo(Log.Logger.Information, LogLevel.Information);
        }
    });*/



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

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

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
