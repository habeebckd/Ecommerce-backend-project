using E_Commerce.Dto.product;

namespace E_Commerce.Service.ProductService
{
    public interface IProductServices
    {
        Task AddProduct(AddProductDto addpro, IFormFile image);
        Task<List<ProductWithCategoryDto>> GetProducts();
        Task<List<ProductWithCategoryDto>> FeaturedPro();
        //Rating Above 4
        Task<ProductWithCategoryDto> GetProductById(int id);
        Task<List<ProductWithCategoryDto>> GetProductsByCategoryName(string categoryname);
        Task<bool> DeleteProduct(int id);
        Task UpdatePro(int id, AddProductDto addpro, IFormFile image);
        Task<List<ProductWithCategoryDto>> SearchProduct(string search);
        Task<List<ProductWithCategoryDto>> HotDeals();
    }
}
