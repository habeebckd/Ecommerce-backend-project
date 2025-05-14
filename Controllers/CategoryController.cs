using E_Commerce.ApiResponse;
using E_Commerce.Dto.category;
using E_Commerce.Model;
using E_Commerce.Service.CategoryService;
using E_Commerce.Service.CategoryServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryServices _categoryServices;
        public CategoryController(ICategoryServices categoryServices) 
        {
            _categoryServices = categoryServices;
        }
        [HttpGet("getCategories")]
        public async Task<IActionResult> GetCat() 
        {
            try
            {
                var categoryList = await _categoryServices.GetCategories();
                return Ok(new ApiResponse<IEnumerable<CategoryDto>>(true, "categories fetched", categoryList, null));
            }
            catch (Exception ex) 
            {
                return StatusCode(500, ex.Message);
            }
        }
        [Authorize(Roles ="Admin")]
        [HttpPost("AddCategory")]
        public async Task<IActionResult>Add_category(CatAddDto newcategory) 
        {
            try
            {
                var res = await _categoryServices.AddCategory(newcategory);
                return Ok(res);
            }
            catch (Exception ex) 
            {
                return StatusCode(500, ex.Message);
            }
        }
        [Authorize(Roles ="Admin")]
        [HttpDelete("DeleteCategory{id}")]
        public async Task<IActionResult> Delete_Cat(int id) 
        {
            try
            {
                var res = await _categoryServices.RemoveCategory(id);
                return Ok(res);
            }
            catch (Exception ex) 
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
