namespace E_Commerce.Dto.Cart
{
    public class CartViewDto
    {
        
        public int ProductId { get; set; }
        public string ?ProductName { get; set; }
        public int Price {  get; set; }
        public string? ProductImage {  get; set; }
        public int TotalAmount {  get; set; }
        public int OrginalPrize {  get; set; }
        public int Quantity {  get; set; }
    }
}
