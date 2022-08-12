namespace HoffTestTask.Infrastructure.Results;

public interface IResult
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public int StatusCode { get; set; }
}

public interface IResult<T> : IResult
{
}