using AutoFixture;
using HoffTestTask.Application.Services.Exchange.Cbr;
using HoffTestTask.Infrastructure.Enums;
using HoffTestTask.Infrastructure.Options;
using HoffTestTask.Infrastructure.Services.MemoryCache;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Moq;

namespace HoffTestTask.Application.Tests.Services;

public class CbrExchangeServiceTests
{
    private const int Expiration = 1;
    private readonly IMemoryCache _memoryCache;
    private readonly Fixture _fixture = new();
    private CbrExchangeService _cbrExchangeService;
    private readonly Mock<IMemoryCacheService> _memoryCacheServiceMock = new();

    private readonly IOptions<ExpirationOptions> _expirationOptions =
        Options.Create(new ExpirationOptions { Expiration = Expiration });

    public CbrExchangeServiceTests()
    {
        var serviceProvider = new ServiceCollection()
            .AddMemoryCache()
            .BuildServiceProvider();
        _memoryCache = serviceProvider.GetService<IMemoryCache>();
    }
    
    [Theory]
    [InlineData(ExchangeDate.Today)]
    [InlineData(ExchangeDate.Tomorrow)]
    [InlineData(ExchangeDate.Yesterday)]
    [InlineData(ExchangeDate.DayBeforeYesterday)]
    public async Task GetExchange_Success(ExchangeDate exchangeDate)
    {
        const string url = "http://www.cbr.ru/scripts/XML_daily.asp";
        const string currencyCode = "R01090B";
        _cbrExchangeService =
            new CbrExchangeService(url, currencyCode, new MemoryCacheService(_memoryCache, _expirationOptions));

        var result = await _cbrExchangeService.GetExchangeAsync(exchangeDate);

        Assert.NotNull(result);
        Assert.IsType<decimal>(result.Data);
        Assert.True(result.Success);
    }

    [Theory]
    [InlineData(ExchangeDate.Today)]
    [InlineData(ExchangeDate.Tomorrow)]
    [InlineData(ExchangeDate.Yesterday)]
    [InlineData(ExchangeDate.DayBeforeYesterday)]
    public async Task GetExchange_Failure(ExchangeDate exchangeDate)
    {
        var url = _fixture.Create<string>();
        var currencyCode = _fixture.Create<string>();
        _cbrExchangeService =
            new CbrExchangeService(url, currencyCode, new MemoryCacheService(_memoryCache, _expirationOptions));

        var result = await _cbrExchangeService.GetExchangeAsync(exchangeDate);

        Assert.NotNull(result);
        Assert.NotEmpty(result.Message);
        Assert.False(result.Success);
    }
}