namespace CW21Ta23.Domain.Entities;

public class Customer : BaseEntity
{
    public string FullName { get; set; } 
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime CreatAt { get; set; } = DateTime.UtcNow;
    public List<Order>? Orders { get; set; } = new List<Order>();
}