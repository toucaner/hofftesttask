using HoffTestTask.Infrastructure.Enums;

namespace HoffTestTask.Infrastructure.Options;

public class ExchangeServiceOptions
{
    public ExchangeServiceType ExchangeServiceType { get; set; }
    public string Url { get; set; }
}