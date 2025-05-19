namespace E_Commerce.Dto.Order
{
    public class OrderSearchDto
    {
        public int? OrderId { get; set; }
        public string? OrderStatus { get; set; }
        public DateTime? OrderDatefirst { get; set; }
        public DateTime? OrderDateEnd { get; set; }
        public int? userId { get; set; }
    }
}
