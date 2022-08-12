using HoffTestTask.Infrastructure.Options;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace HoffTestTask.Infrastructure.Services.MemoryCache;

public class MemoryCacheService : IMemoryCacheService
{
    private readonly IMemoryCache _memoryCache;
    private readonly ExpirationOptions _expirationOptions;

    public MemoryCacheService(IMemoryCache memoryCache,
        IOptions<ExpirationOptions> expirationOptions)
    {
        _memoryCache = memoryCache;
        _expirationOptions = expirationOptions.Value;
    }

    public async Task<T> GetAsync<T>(object key, Func<Task<T>> func)
    {
        if (_memoryCache.TryGetValue(key, out T result))
            return result;

        result = await func();

        if (result != null)
            Set(key, result);

        return result;
    }

    private void Set(object key, object data)
    {
        _memoryCache.Set(key, data,
            new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(_expirationOptions.Expiration)));
    }
}