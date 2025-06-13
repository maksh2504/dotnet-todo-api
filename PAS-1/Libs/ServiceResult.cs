namespace PAS_1.Libs;

public class ServiceResult<T>
{
    public bool Success => Error == null;
    public T? Data { get; init; }
    public ServiceError? Error { get; init; }

    public static ServiceResult<T> Ok(T data) => new() { Data = data };
    public static ServiceResult<T> Fail(ServiceError error) => new() { Error = error };
}
