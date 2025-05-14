using E_Commerce.ApiResponse;
using E_Commerce.Dto.WishList;
using E_Commerce.Service.WishList;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishListController : ControllerBase
    {
        private readonly IWishListServices _services;
        public WishListController(IWishListServices services)
        {
            _services = services;
        }



        [HttpGet("GetWhishList")]
        [Authorize]
        public async Task<IActionResult> GetWishList()
        {
            try
            {
                int u_id = Convert.ToInt32(HttpContext.Items["Id"]);
                var res = await _services.GetAllWishItems(u_id);


                if (res.Count > 0)
                {
                    return Ok(new ApiResponse<IEnumerable<WishListViewDto>>(true, "whislist fetched", res, null));
                }

                return Ok(new ApiResponse<IEnumerable<WishListViewDto>>(true, "no items in whislist ", res, null));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpPost("AddOrRemove/{pro_id}")]
        [Authorize]
        public async Task<IActionResult> Add(int pro_id)
        {
            try
            {
                int u_id = Convert.ToInt32(HttpContext.Items["Id"]);
                var res = await _services.AddOrRemove(u_id, pro_id);

                return Ok(res);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("RemoveWishlist/{pro_id}")]
        [Authorize]
        public async Task<IActionResult> remove(int pro_id)
        {
            try
            {
                int u_id = Convert.ToInt32(HttpContext.Items["Id"]);
                var res = await _services.RemoveFromWishList(u_id, pro_id);

                if (res.IsSuccess)
                {
                    return Ok(res);
                }

                return BadRequest(res);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
