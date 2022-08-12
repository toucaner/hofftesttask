using HoffTestTask.Infrastructure.Enums;

namespace HoffTestTask.Infrastructure.Extensions;

public static class ExchangeDateExtensions
{
    public static DateTime GetDateTime(this ExchangeDate exchangeDateType)
    {
        var now = DateTime.UtcNow;
        return exchangeDateType switch
        {
            ExchangeDate.Today => now,
            ExchangeDate.Tomorrow => now.AddDays(1),
            ExchangeDate.Yesterday => now.AddDays(-1),
            ExchangeDate.DayBeforeYesterday => now.AddDays(-2),
            _ => throw new ArgumentOutOfRangeException(nameof(exchangeDateType), exchangeDateType, "Тип не определен")
        };
    }
}