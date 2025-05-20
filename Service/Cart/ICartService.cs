using E_Commerce.ApiResponse;
using E_Commerce.Dto;
using E_Commerce.Dto.Cart;

namespace E_Commerce.Service.Cart
{
    public interface ICartService
    {
        Task<CartWithTotalPrice> GetAllCartItems(int userId);
        Task<ApiResponse<CartViewDto>> AddToCart(int userId, int productId);
        Task<ApiResponse<string>> RemoveFromCart(int userId, int productId);
        Task<ApiResponse<CartViewDto>> IncraseQuantity(int userId, int productId);
        Task<ApiResponse<CartViewDto>> DecreaseQuantity(int userId, int productId);
    }
}
