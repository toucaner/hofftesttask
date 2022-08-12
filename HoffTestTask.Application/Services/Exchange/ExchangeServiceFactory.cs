using HoffTestTask.Application.Services.Exchange.Cbr;
using HoffTestTask.Features.Services.Exchange;
using HoffTestTask.Infrastructure.Enums;
using HoffTestTask.Infrastructure.Options;
using HoffTestTask.Infrastructure.Services.MemoryCache;
using Microsoft.Extensions.Options;

namespace HoffTestTask.Application.Services.Exchange;

public class ExchangeServiceFactory : IExchangeServiceFactory
{
    private readonly ExchangeOptions _exchangeOptions;
    private readonly IMemoryCacheService _memoryCacheService;

    public ExchangeServiceFactory(
        IOptions<ExchangeOptions> exchangeOptions, 
        IMemoryCacheService memoryCacheService)
    {
        _memoryCacheService = memoryCacheService;
        _exchangeOptions = exchangeOptions.Value;
    }

    public IExchangeService GetExchangeService(ExchangeServiceType type)
    {
        var service = _exchangeOptions.Services.FirstOrDefault(s => s.ExchangeServiceType.Equals(type)) ??
                      _exchangeOptions.Services.FirstOrDefault(s =>
                          s.ExchangeServiceType.Equals(_exchangeOptions.DefaultExchangeService));

        return service.ExchangeServiceType switch
        {
            ExchangeServiceType.Cbr => new CbrExchangeService(service.Url, _exchangeOptions.CurrencyCode, _memoryCacheService),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, "Тип сервиса не определен")
        };
    }
}