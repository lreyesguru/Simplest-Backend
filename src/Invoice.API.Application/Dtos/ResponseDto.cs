namespace Invoice.API.Application;

public class ResponseDto<T>
{

    public bool success { get; set; }
    public string message { get; set; }
    public T data { get; set; }
    public ResponseDto(bool success, string message, T? data)
    {
        this.success = success;
        this.message = message;
        this.data = data;
    }

    public static ResponseDto<T> Ok(T data, string message = "success") => new ResponseDto<T>(true, message, data);
    public static ResponseDto<T> Fail(string message) => new ResponseDto<T>(false, message, default);
}