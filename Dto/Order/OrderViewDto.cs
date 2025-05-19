using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Dto.Order
{
    public class OrderViewDto
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string? OrderStatus { get; set; }
        [Required]
        public string? OrderString { get; set; }

        [Required]
        public string? TransactionId { get; set; }
        public List<OrderItemDto> Items { get; set; }

        public decimal Total { get; set; }
    }
}
