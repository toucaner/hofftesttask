namespace HoffTestTask.Infrastructure.Results;

public class Result : IResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }

        public static Result Ok(string message = null)
            => Create(message, 200, true);

        public static Result Created(string message = null)
            => Create(message, 201, true);

        public static Result Updated(string message = null)
            => Create(message, 204, true);

        public static Result Bad(string message)
            => Create(message, 400);

        public static Result UnAuthorized(string message)
            => Create(message, 401);

        public static Result Forbidden(string message)
            => Create(message, 403);

        public static Result NotFound(string message)
            => Create(message, 404);

        public static Result ImTeapot(string message = default)
            => Create(message, 418);

        public static Result Failed(string message, int code = 500)
            => Create(message, code);

        public static Result Failed(IResult result)
            => Create(result.Message, result.StatusCode);

        public static Result Internal(string message)
            => Create(message, 500);
        
        private static Result Create(
            string message,
            int code = 422,
            bool success = false)
            => new() { Message = message, StatusCode = code, Success = success };
    }

public class Result<T> : IResult<T>
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public T Data { get; set; }
    public int StatusCode { get; set; }

    public static Result<T> Ok(T data, string message = null)
        => Create(message, 200, data, true);

    public static Result<T> Created(T data, string message = null)
        => Create(message, 201, data, true);

    public static Result<T> Updated(T data = default, string message = null)
        => Create(message, 204, data, true);

    public static Result<T> Bad(string message)
        => Create(message, 400);

    public static Result<T> UnAuthorized(string message)
        => Create(message, 401);

    public static Result<T> Forbidden(string message)
        => Create(message, 403);

    public static Result<T> NotFound(string message)
        => Create(message, 404);

    public static Result<T> ImTeapot(string message = default)
        => Create(message, 418);

    public static Result<T> Failed(string message, int code = 500)
        => Create(message, code);

    public static Result<T> Failed(IResult result)
        => Create(result.Message, result.StatusCode);

    public static Result<T> Internal(string message)
        => Create(message, 500);

    private static Result<T> Create(
        string message,
        int code = 422,
        T data = default,
        bool success = false)
        => new() { Message = message, StatusCode = code, Success = success, Data = data };
}