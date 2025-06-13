namespace PAS_1.Libs
{
    public class ServiceError
    {
        public int StatusCode { get; init; }
        public string Message { get; init; } = string.Empty;

        public static ServiceError NotFound(string message) =>
            new() { StatusCode = 404, Message = message };

        public static ServiceError Unauthorized(string message) =>
            new() { StatusCode = 401, Message = message };

        public static ServiceError BadRequest(string message) =>
            new() { StatusCode = 400, Message = message };

        public static ServiceError Conflict(string message) =>
            new() { StatusCode = 409, Message = message };

        public static ServiceError Internal(string message) =>
            new() { StatusCode = 500, Message = message };
    }
}


