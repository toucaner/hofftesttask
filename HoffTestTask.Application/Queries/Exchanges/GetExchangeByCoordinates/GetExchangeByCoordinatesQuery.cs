using HoffTestTask.Infrastructure.Results;
using MediatR;

namespace HoffTestTask.Application.Queries.Exchanges.GetExchangeByCoordinates;

public class GetExchangeByCoordinatesQuery : IRequest<Result<decimal>>
{
    public int X { get; set; }
    public int Y { get; set; }
}