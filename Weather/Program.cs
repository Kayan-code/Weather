using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Weather.Context;
using testeaec.Repository.WeatherData;
using testeaec.Repository;
using testeaec.Services;
using Weather.Repository;
using Weather.Services;

var builder = WebApplication.CreateBuilder(args);

// Configure logging providers
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();
builder.Services.AddHttpClient();
builder.Services.AddDbContext<WeatherContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped<IWeatherAirportService, WeatherAirportService>();
builder.Services.AddScoped<IWeatherCityService, WeatherCityService>();
builder.Services.AddScoped<IWeatherAirportRepository, WeatherAirportRepository>();
builder.Services.AddScoped<IWeatherCityRepository, WeatherCityRepository>();
builder.Services.AddScoped<ILogRepository, LogRepository>();


builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckleu
builder.Services.AddEndpointsApiExplorer();

var swaggerVersion = "v1";
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc(swaggerVersion, new OpenApiInfo
    {
        Version = swaggerVersion,
        Title = "Weather City or Airport API",
        Description = "NET Core Web API to return the weather in a city or airport"
    });
    options.ResolveConflictingActions(x => x.First());
    //var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    //options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));    
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (app.Environment.IsProduction())
{
    app.UseSwagger(c =>
    {
        c.RouteTemplate = "swagger/{documentName}/swagger.json";
        c.PreSerializeFilters.Add((swaggerDoc, httpReq) => swaggerDoc.Servers = new List<OpenApiServer>
        {
            new() { Url = $"https://{httpReq.Host.Value}" }
        });
    });
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint(swaggerVersion + "/swagger.json",
            "Weather City or Airport API " + swaggerVersion.ToUpper());
    });
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
