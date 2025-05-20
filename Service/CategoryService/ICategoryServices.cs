using E_Commerce.ApiResponse;
using E_Commerce.Dto.category;

namespace E_Commerce.Service.CategoryService
{
    public interface ICategoryServices
    {

        Task<List<CategoryDto>> GetCategories();
        Task<ApiResponse<CatAddDto>> AddCategory(CatAddDto categorty);

        Task<ApiResponse<string>> RemoveCategory(int id);
    }
}
