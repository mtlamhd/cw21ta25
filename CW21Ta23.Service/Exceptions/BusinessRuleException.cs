namespace CW21Ta23.Service.Exceptions;

public class BusinessRuleException : BaseAppException
{
    public BusinessRuleException(string message) : base(message,409)
    {
    }
    public static BusinessRuleException CannotDelete(string entityName, string reason)
    {
        return new BusinessRuleException($"Cannot delete {entityName} because {reason}.");
    }
}