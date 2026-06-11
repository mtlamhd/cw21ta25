namespace CW21Ta23.Domain.Entities;

public class BaseEntity
{
    public int Id  { get; set; }
    
    public bool IsDeleted { get; set; }
}