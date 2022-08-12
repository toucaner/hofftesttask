using HoffTestTask.Infrastructure.Enums;
using HoffTestTask.Infrastructure.Results;

namespace HoffTestTask.Application.Services.Coordinate;

public interface ICoordinateService
{
    public Result<ExchangeDate> GetDateByCoordinates(Application.Services.Coordinate.Models.Coordinate coordinate);
}