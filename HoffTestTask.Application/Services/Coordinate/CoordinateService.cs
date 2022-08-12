using HoffTestTask.Infrastructure.Enums;
using HoffTestTask.Infrastructure.Options;
using HoffTestTask.Infrastructure.Results;
using Microsoft.Extensions.Options;

namespace HoffTestTask.Application.Services.Coordinate;

public class CoordinateService : ICoordinateService
{
    private readonly GeometryOptions _geometryOptions;
    
    public CoordinateService(IOptions<GeometryOptions> geometryOptions)
    {
        _geometryOptions = geometryOptions.Value;
    }
    
    public Result<ExchangeDate> GetDateByCoordinates(Models.Coordinate coordinate)
    {
        var x = coordinate.X;
        var y = coordinate.Y;
        var radius = _geometryOptions.Radius;

        var isCoordinatesIncorrect = x.Equals(default) || y.Equals(default) || x > radius || y > radius;
        var isCoordinatesNotInCircle = Math.Pow(x, 2) + Math.Pow(y, 2) > radius;
        if (isCoordinatesIncorrect || isCoordinatesNotInCircle)
            return Result<ExchangeDate>.Failed("Координаты заданы не верно");

        return x switch
        {
            > default(int) when y > default(int) => Result<ExchangeDate>.Ok(ExchangeDate.Today),
            < default(int) when y > default(int) => Result<ExchangeDate>.Ok(ExchangeDate.Yesterday),
            < default(int) when y < default(int) => Result<ExchangeDate>.Ok(ExchangeDate.DayBeforeYesterday),
            > default(int) when y < default(int) => Result<ExchangeDate>.Ok(ExchangeDate.Tomorrow),
        };
    }
}