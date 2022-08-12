using HoffTestTask.Application.Services.Coordinate;
using HoffTestTask.Application.Services.Coordinate.Models;
using HoffTestTask.Application.Services.Exchange;
using HoffTestTask.Features.Services.Exchange;
using HoffTestTask.Infrastructure.Results;
using MediatR;

namespace HoffTestTask.Application.Queries.Exchanges.GetExchangeByCoordinates;

public class GetExchangeByCoordinatesQueryHandler : IRequestHandler<GetExchangeByCoordinatesQuery, Result<decimal>>
{
    private readonly ICoordinateService _coordinateService;
    private readonly IExchangeServiceFactory _exchangeServiceFactory;

    public GetExchangeByCoordinatesQueryHandler(
        ICoordinateService coordinateService,
        IExchangeServiceFactory exchangeServiceFactory)
    {
        _coordinateService = coordinateService;
        _exchangeServiceFactory = exchangeServiceFactory;
    }

    public async Task<Result<decimal>> Handle(GetExchangeByCoordinatesQuery request,
        CancellationToken cancellationToken)
    {
        var exchangeDateResult =
            _coordinateService.GetDateByCoordinates(new Coordinate { X = request.X, Y = request.Y });
        if (!exchangeDateResult.Success)
            return Result<decimal>.Failed(exchangeDateResult.Message);

        var exchangeService = _exchangeServiceFactory.GetExchangeService();
        var exchangeResult = await exchangeService.GetExchangeAsync(exchangeDateResult.Data, cancellationToken)
            .ConfigureAwait(false);
        if (!exchangeResult.Success)
            Result<decimal>.Failed(exchangeResult.Message);

        // Значение иностранной валюты выводить в значении к одному рублю РФ
        return Result<decimal>.Ok(1 / exchangeResult.Data);
    }
}