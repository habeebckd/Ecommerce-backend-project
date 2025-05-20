using E_Commerce.ApiResponse;
using E_Commerce.Context;
using E_Commerce.Dto.WishList;
using E_Commerce.Service.WishList;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Service.WishListServices
{
    public class WishListServices : IWishListServices
    {
        private readonly AppDbContext _context;
        public WishListServices(AppDbContext context) 
        {
            _context = context;
        }


        public async Task<ApiResponse<string>> AddOrRemove(int u_id, int pro_id)
        {
            try
            {
                var isExists = await _context.wishList
                    .Include(a => a._Product)
                    .FirstOrDefaultAsync(b => b.ProductId == pro_id && b.UserId == u_id);

                if (isExists == null)
                {
                    var add_wish = new Model.WishList
                    {
                        UserId = u_id,
                        ProductId = pro_id,
                    };

                    _context.wishList.Add(add_wish);
                    await _context.SaveChangesAsync();
                    return new ApiResponse<string>(true, "Item added to the wishList", "done", null);
                }
                else
                {
                    _context.wishList.Remove(isExists);
                    await _context.SaveChangesAsync();

                    return new ApiResponse<string>(true, "Item removed from wishList", "done", null);
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while updating the wishlist.", ex);
            }
        }



        public async Task<List<WishListViewDto>> GetAllWishItems(int u_id)
        {
            try
            {
                var items = await _context.wishList
                    .Include(a => a._Product)
                    .ThenInclude(b => b._Category)
                    .Where(c => c.UserId == u_id)
                    .ToListAsync();

                if (items != null)
                {
                    var p = items.Select(a => new WishListViewDto
                    {
                        Id = a.Id,
                        ProductId = a._Product.Id,
                        ProductName = a._Product.ProductName,
                        ProductDescription = a._Product.ProductDescription,
                        Price = a._Product.ProductPrize,
                        OfferPrice = a._Product.offerPrice,
                        ProductImage = a._Product.ImageUrl,
                        CategoryName = a._Product._Category?.CategoryName
                    }).ToList();

                    return p;
                }
                else
                {
                    return new List<WishListViewDto>();
                }

            }

            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }



        public async Task<ApiResponse<string>> RemoveFromWishList(int u_id, int pro_id)
        {
            try
            {
                var isExists = await _context.wishList
                  .Include(a => a._Product)
                  .FirstOrDefaultAsync(b => b.Id == pro_id && b.UserId == u_id);

                if (isExists != null)
                {
                    _context.wishList.Remove(isExists);
                    await _context.SaveChangesAsync();
                    return new ApiResponse<string>(true, "Item removed from wishList", "done", null);
                }

                return new ApiResponse<string>(false, "Product not found", "", null);


            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }




    }
}
