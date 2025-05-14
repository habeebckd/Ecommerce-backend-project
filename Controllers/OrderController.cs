using E_Commerce.ApiResponse;
using E_Commerce.Dto.Order;
using E_Commerce.Service.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderServices _orderServices;
        public OrderController(IOrderServices orderServices)
        {
            _orderServices = orderServices;
        }


        [Authorize(Roles = "Admin")]
        [HttpPatch("manage-order-status/{oid}")]
        public async Task<IActionResult> UpdateOrderStatus(int oid, [FromBody] string newStatus)
        {
            try
            {
                var result = await _orderServices.UpdateOrderStatus(oid, newStatus);
                return Ok(new ApiResponse<string>(true, "Order status updated successfully", result, null));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<string>(false, "Failed to update order status", null, ex.InnerException?.Message ?? ex.Message));
            }
        }



        [Authorize]
        [HttpPost("Place-order-individual/{pro_id}")]
        public async Task<IActionResult> individual_probuy(int pro_id, CreateOrderDto dto)
        {
            try
            {
                if (dto == null)
                {
                    return BadRequest("Order-details-are-required");
                }
                var user_id = Convert.ToInt32(HttpContext.Items["Id"]);
                var res = await _orderServices.Indidvidual_ProductBuy(user_id, pro_id, dto);

                return Ok(new ApiResponse<string>(true, "Product purchased successfully", "done", null));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }




        [HttpPost(" Place-order-all")]
        [Authorize]
        public async Task<IActionResult> PlaceOrder(CreateOrderDto createOrderDto)
        {
            try
            {
                var user_id = Convert.ToInt32(HttpContext.Items["Id"]);
                var res = await _orderServices.OrderFullCart(user_id, createOrderDto);
                return Ok(new ApiResponse<string>(true, "successfully placed", "done", null));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpGet("Get-order-Details")]
        public async Task<IActionResult> GetOrderDetails()
        {
            try
            {
                var u_id = Convert.ToInt32(HttpContext.Items["Id"]);
                var res = await _orderServices.GetOrderDetails(u_id);
                return Ok(new ApiResponse<IEnumerable<OrderViewDto>>(true, "successfully ordered", res, null));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpGet("Get-order-Details-Admin")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetOrderDetailsAdmin()
        {
            try
            {
                var res = await _orderServices.GetOrderDetailsAdmin();
                if (res.Count < 0)
                {
                    return BadRequest(new ApiResponse<string>(false, "no order found", null, null));
                }
                return Ok(new ApiResponse<IEnumerable<OrderAdminViewDto>>(true, "successfully", res, null));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpGet("Total Revenue")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> TotalRevenue()
        {
            try
            {
                var res = await _orderServices.TotalRevenue();
                return Ok(new ApiResponse<decimal>(true, "successfully", res, null));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }




        [HttpGet("Total-Products-Sold")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> TotalProductsPurchased()
        {
            try
            {
                var res = await _orderServices.TotalProductsPurchased();
                return Ok(new ApiResponse<int>(true, "successfully", res, null));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }




        [HttpGet("GetOrderDetailsAdmin-ByuserId/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetOrderDetailsAdmin_byuserId(int id)
        {
            try
            {
                var orderDetails = await _orderServices.GetOrderDetailsAdmin_byuserId(id);
                if (orderDetails == null)
                {
                    return NotFound("User not found");
                }
                return Ok(new ApiResponse<IEnumerable<OrderViewDto>>(true, "done", orderDetails, null));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
