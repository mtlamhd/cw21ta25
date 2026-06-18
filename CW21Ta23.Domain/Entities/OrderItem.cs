using System.ComponentModel.DataAnnotations;

namespace CW21Ta23.Domain.Entities;

public class OrderItem : BaseEntity
{
    [Required]
    public int OrderId { get; set; }

    [Required]
    public int BookId { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Quantity باید بیشتر از صفر باشد")]
    public int Quantity { get; set; }

    [Required]
    public decimal UnitPrice { get; set; }

// Navigation Properties
    public Order? Order { get; set; }
    public Book? Book { get; set; }
}