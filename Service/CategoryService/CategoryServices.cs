using AutoMapper;
using E_Commerce.Context;
using E_Commerce.ApiResponse;
using Microsoft.EntityFrameworkCore;
using E_Commerce.Model;
using E_Commerce.Service.CategoryService;
using E_Commerce.Dto.category;

namespace E_Commerce.Service.CategoryServices
{
    public class CategoryServices : ICategoryServices
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public CategoryServices(AppDbContext context, IMapper mapper) 
        {
            _context = context;
            _mapper = mapper;
        }
       public async Task<List<CategoryDto>> GetCategories() 
        {
            var cat = await _context.Categories.ToListAsync();
            var res = _mapper.Map<List<CategoryDto>>(cat);
            return res;
        }
        public async Task<ApiResponse<CatAddDto>> AddCategory(CatAddDto categorty) 
        {
            var isExists = await _context.Categories.FirstOrDefaultAsync(a=>a.CategoryName == categorty.CategoryName);
            if (isExists != null) 
            {
                return new ApiResponse<CatAddDto>(false, "Category already Exists", null, "add another category");
            }
            var res = _mapper.Map<Category>(categorty);
            _context.Categories.Add(res);
            await _context.SaveChangesAsync();
            var response =_mapper.Map<CatAddDto>(res);
            return new ApiResponse<CatAddDto>(true, "New Category Added In To Database", response, null);
        }
        public async Task<ApiResponse<string>> RemoveCategory(int id) 
        {
            try
            {
                var res = await _context.Categories.FirstOrDefaultAsync(q => q.Id == id);
                var pro = await _context.Categories.Where(q => q.Id == id).ToListAsync();

                if (res == null)
                {
                    return new ApiResponse<string>(false, "Category Not Found", "", "check agin");
                }
                else
                {
                    //_context.products.RemoveRange(pro);
                    _context.Categories.Remove(res);
                    await _context.SaveChangesAsync();
                    return new ApiResponse<string>(true, "Done", "Category Deleted", null);
                }
            }
            catch (Exception ex) 
            {
                throw new Exception($"An Error Occured While Saving Changes : {ex.InnerException.Message ?? ex.Message}");
            }
        }

    }
}
