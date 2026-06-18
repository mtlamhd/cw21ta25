namespace CW21Ta23.Service.Exceptions;

public class ItemNotFoundException : BaseAppException
{
    public ItemNotFoundException(string itemName, int id) : base($"{itemName} with id {id} was not found",404)
    {
    }
    
}