namespace CW21Ta23.Service.Exceptions;

public class ConflictException : BaseAppException
{
    public ConflictException(string message) : base(message,409)
    {
        
    }

    public ConflictException(string fieldName,string value)
        : base($"The {fieldName} with value '{value}' already exists.",409)
    {
        
    }
}