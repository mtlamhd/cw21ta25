using System.ComponentModel.DataAnnotations;

namespace CW21Ta23.Domain.Entities;

public class Order : BaseEntity
{
    
    [Required]
    public int CustomerId { get; set; }

    [Required]
    public DateTime OrderDate { get; set; }

    [Required]
    public decimal TotalAmount { get; set; }

    [Required]
    [MaxLength(50)]
    public string Status { get; set; }

// Navigation Properties
    public Customer? Customer { get; set; }
    public ICollection<OrderItem>? OrderItems { get; set; } = new List<OrderItem>();
}