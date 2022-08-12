using HoffTestTask.Infrastructure.Enums;

namespace HoffTestTask.Application.Services.Exchange;

public interface IExchangeServiceFactory
{
    public IExchangeService GetExchangeService(ExchangeServiceType type = ExchangeServiceType.Default);
}