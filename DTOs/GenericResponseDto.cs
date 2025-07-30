namespace DTOs;

public class GenericResponseDto<T>
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public T? Data { get; set; }

    public static GenericResponseDto<T> Fail(string message) =>
        new GenericResponseDto<T> { Success = false, Message = message };

    public static GenericResponseDto<T> Ok(T data, string message = "") =>
        new GenericResponseDto<T> { Success = true, Message = message, Data = data };
}
