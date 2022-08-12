using HoffTestTask.Infrastructure.Enums;
using HoffTestTask.Infrastructure.Results;
using HoffTestTask.Infrastructure.Services.MemoryCache;

namespace HoffTestTask.Application.Services.Exchange;

public abstract class ExchangeServiceBase : IExchangeService
{
    protected readonly string Url;
    protected readonly string CurrencyCode;
    protected readonly IMemoryCacheService MemoryCacheService;

    protected ExchangeServiceBase(string url, string currencyCode, IMemoryCacheService memoryCacheService)
    {
        Url = url;
        CurrencyCode = currencyCode;
        MemoryCacheService = memoryCacheService;
    }

    public abstract Task<Result<decimal>> GetExchangeAsync(ExchangeDate exchangeDate,
        CancellationToken cancellationToken = default);
}