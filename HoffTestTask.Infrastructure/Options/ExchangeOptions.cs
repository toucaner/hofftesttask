using HoffTestTask.Infrastructure.Enums;

namespace HoffTestTask.Infrastructure.Options;

public class ExchangeOptions
{
    public ExchangeServiceType DefaultExchangeService { get; set; }
    public string CurrencyCode { get; set; }
    public List<ExchangeServiceOptions> Services { get; set; }
}