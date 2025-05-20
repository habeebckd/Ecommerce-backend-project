using E_Commerce.ApiResponse;
using E_Commerce.Dto.WishList;

namespace E_Commerce.Service.WishList
{
    public interface IWishListServices
    {
        Task<ApiResponse<string>> AddOrRemove(int u_id, int Pro_id);
        Task<ApiResponse<string>> RemoveFromWishList(int u_id, int pro_id);
        Task<List<WishListViewDto>> GetAllWishItems(int u_id);
    }
}
