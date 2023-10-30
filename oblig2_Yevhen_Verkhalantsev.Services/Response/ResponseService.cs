namespace oblig2_Yevhen_Verkhalantsev.Services.Response;

public class ResponseService
{
    public bool IsError { get; set; }
    public string ErrorMessage { get; set; }
    
    public static ResponseService Ok()
    {
        return new ResponseService
        {
            IsError = false,
            ErrorMessage = string.Empty
        };
    }
    
    public static ResponseService Error(string errorMessage)
    {
        return new ResponseService
        {
            IsError = true,
            ErrorMessage = errorMessage
        };
    }
}

public class ResponseService<T>
{
    
    public bool IsError { get; set; }
    public string ErrorMessage { get; set; }
    
    public T Value { get; set; }
    
    public static ResponseService<T> Ok(T data)
    {
        return new ResponseService<T>
        {
            IsError = false,
            ErrorMessage = string.Empty,
            Value = data
        };
    }
    
    public static ResponseService<T> Error(string errorMessage)
    {
        return new ResponseService<T>
        {
            IsError = true,
            ErrorMessage = errorMessage,
            Value = default
        };
    }
}