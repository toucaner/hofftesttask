using System.Xml.Serialization;
using HoffTestTask.Features.Services.Exchange.Models;
using HoffTestTask.Infrastructure.Enums;
using HoffTestTask.Infrastructure.Extensions;
using HoffTestTask.Infrastructure.Results;
using HoffTestTask.Infrastructure.Services.MemoryCache;

namespace HoffTestTask.Application.Services.Exchange.Cbr;

public class CbrExchangeService : ExchangeServiceBase
{
    public CbrExchangeService(string url, string currencyCode, IMemoryCacheService memoryCacheService) :
        base(url, currencyCode, memoryCacheService)
    {
    }

    public override async Task<Result<decimal>> GetExchangeAsync(ExchangeDate exchangeDate,
        CancellationToken cancellationToken = default)
    {
        var result = await MemoryCacheService.GetAsync(exchangeDate, async () =>
        {
            var exchangeRatesResult =
                await GetExchangeRatesAsync(exchangeDate, cancellationToken).ConfigureAwait(false);
            if (!exchangeRatesResult.Success)
                return Result<decimal>.Failed(exchangeRatesResult.Message);

            var rate = exchangeRatesResult.Data.FirstOrDefault(r => r.Code.Equals(CurrencyCode));

            if (rate == null)
                return Result<decimal>.Failed("Курс валюты не найден");

            decimal.TryParse(rate.Value, out var rateResult);
            return Result<decimal>.Ok(rateResult);
        }).ConfigureAwait(false);

        return !result.Success ? Result<decimal>.Failed(result.Message) : Result<decimal>.Ok(result.Data);
    }

    private async Task<Result<Valute[]>> GetExchangeRatesAsync(ExchangeDate exchangeDate,
        CancellationToken cancellationToken = default)
    {
        CbrResponse result;
        var date = exchangeDate.GetDateTime();

        try
        {
            using var client = new HttpClient();
            using var response = await client.GetAsync($"{Url}?date_req={date:dd/MM/yyyy}", cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                using var content = response.Content;
                var buffer = await content.ReadAsByteArrayAsync(cancellationToken).ConfigureAwait(false);
                var xml = System.Text.Encoding.Default.GetString(buffer);
                var serializer = new XmlSerializer(typeof(CbrResponse), new XmlRootAttribute("ValCurs"));
                using TextReader reader = new StringReader(xml);
                result = (CbrResponse)serializer.Deserialize(reader);
            }
            else
            {
                return Result<Valute[]>.Failed(response.ReasonPhrase, (int)response.StatusCode);
            }
        }
        catch (Exception e)
        {
            return Result<Valute[]>.Failed(e.Message);
        }

        return Result<Valute[]>.Ok(result?.ExchangeRates);
    }
}