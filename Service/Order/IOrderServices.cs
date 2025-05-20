using E_Commerce.Dto.Order;

namespace E_Commerce.Service.Order
{
    public interface IOrderServices
    {
        Task<bool> OrderFullCart(int userId, CreateOrderDto createOrderDto);
        Task<bool> Indidvidual_ProductBuy(int userId, int productId, CreateOrderDto order_Dto);
        Task<List<OrderViewDto>> GetOrderDetails(int userId);
        Task<List<OrderAdminViewDto>> GetOrderDetailsAdmin();
        Task<decimal> TotalRevenue();
        Task<int> TotalProductsPurchased();
        Task<List<OrderViewDto>> GetOrderDetailsAdmin_byuserId(int userId);
        Task<string> UpdateOrderStatus(int oId, string newStatus);
    }
}
