namespace HoffTestTask.Infrastructure.Services.MemoryCache;

public interface IMemoryCacheService
{
    public Task<T> GetAsync<T>(object key, Func<Task<T>> func);
}