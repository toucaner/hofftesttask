using HoffTestTask.Infrastructure.Enums;
using HoffTestTask.Infrastructure.Results;

namespace HoffTestTask.Application.Services.Exchange;

public interface IExchangeService
{
    public Task<Result<decimal>> GetExchangeAsync(ExchangeDate exchangeDate, CancellationToken cancellationToken);
}