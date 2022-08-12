using System.Reflection;
using HoffTestTask.Application.Queries.Exchanges.GetExchangeByCoordinates;
using HoffTestTask.Application.Services.Coordinate;
using HoffTestTask.Application.Services.Exchange;
using HoffTestTask.Features.Services.Exchange;
using HoffTestTask.Infrastructure.Options;
using HoffTestTask.Infrastructure.Services.MemoryCache;
using MediatR;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(typeof(GetExchangeByCoordinatesQuery).GetTypeInfo().Assembly);

builder.Services.AddSingleton<IExchangeServiceFactory, ExchangeServiceFactory>();
builder.Services.AddSingleton<IMemoryCacheService, MemoryCacheService>();
builder.Services.AddTransient<ICoordinateService, CoordinateService>();

builder.Services.Configure<ExchangeOptions>(builder.Configuration.GetSection("Exchange"));
builder.Services.Configure<GeometryOptions>(builder.Configuration.GetSection("Geometry"));
builder.Services.Configure<ExpirationOptions>(builder.Configuration.GetSection("MemoryCache"));

builder.Host.UseSerilog((ctx, lc) => lc
    .WriteTo.Console()
    .WriteTo.File($"Logs/HoffTestTask-{DateTime.Now.Date:dd-MM-yyyy}.txt")
    .WriteTo.Seq("http://localhost:5341"));

builder.Services.AddMemoryCache();

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