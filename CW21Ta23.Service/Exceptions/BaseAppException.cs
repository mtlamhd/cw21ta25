namespace CW21Ta23.Service.Exceptions;

public class BaseAppException: Exception 
{
    public int StatusCode { get; private set; }

    public BaseAppException(string message, int statusCode) : base(message)
    {
        StatusCode = statusCode;
    }
    
}