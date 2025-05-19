using E_Commerce.Dto.Cart;

namespace E_Commerce.Dto
{
    public class CartWithTotalPrice
    {
        public int TotalCartPrice {  get; set; }
        public List<CartViewDto> c_items {  get; set; }
    }
}
