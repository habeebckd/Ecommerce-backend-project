using AutoMapper;
using CloudinaryDotNet;
using E_Commerce.CloudinaryServices;
using E_Commerce.Context;
using E_Commerce.Dto.product;
using E_Commerce.Model;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Service.ProductService
{
    public class ProductServices : IProductServices
    {
        private readonly AppDbContext _context;
        private readonly ICloudinaryServices _cloudinary;
        private readonly IMapper _mapper;
        public ProductServices(AppDbContext context, ICloudinaryServices cloudinary, IMapper mapper)
        {
            _context = context;
            _cloudinary = cloudinary;
            _mapper = mapper;
        }

        public async Task<List<ProductWithCategoryDto>> HotDeals()
        {
            try
            {
                var productWithCategory = await _context.Products.Include(x => x._Category).Where(a => (a.ProductPrize - a.offerPrice) > 200).ToListAsync();
                return _mapper.Map<List<ProductWithCategoryDto>>(productWithCategory);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<ProductWithCategoryDto>> FeaturedPro()
        {
            try
            {
                var ProductWithCategory = await _context.Products.Include(x => x._Category).Where(c => c.Rating >= 4).ToListAsync();
                return _mapper.Map<List<ProductWithCategoryDto>>(ProductWithCategory);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task AddProduct(AddProductDto addPro, IFormFile image)
        {
            try
            {
                string imageUrl = await _cloudinary.UploadImageAsync(image);
                var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == addPro.CategoryId);
                if (category == null)
                {
                    throw new Exception("Invalid Category ID");
                }
                var product = new Product
                {
                    ProductName = addPro.ProductName,
                    ProductDescription = addPro.ProductDescription,
                    ProductPrize = addPro.ProductPrice,
                    offerPrice = addPro.OfferPrize,
                    Rating = addPro.Rating,
                    CategoryId = category.Id,
                    ImageUrl = imageUrl,
                };
                await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<ProductWithCategoryDto>> GetProducts()
        {
            try
            {
                var productWithCategory = await _context.Products.Include(x => x._Category).ToArrayAsync();
                if (productWithCategory == null)
                {
                    throw new Exception("product is not exist");
                }
                return _mapper.Map<List<ProductWithCategoryDto>>(productWithCategory);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ProductWithCategoryDto> GetProductById(int id)
        {
            try
            {
                var product = await _context.Products.Include(x=>x._Category).FirstOrDefaultAsync(p => p.Id == id);
                if (product == null)
                {
                    return null;
                }
                return _mapper.Map<ProductWithCategoryDto>(product);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<ProductWithCategoryDto>> GetProductsByCategoryName(string Cat_name)
        {
            try
            {
                if (Cat_name.ToLower() == "all")
                {
                    var allpro = await _context.Products.ToListAsync();
                    if (allpro == null)
                    {
                        return null;
                    }

                    return _mapper.Map<List<ProductWithCategoryDto>>(allpro);
                }

                var catP1 = await _context.Products.Include(x => x._Category)
                    .Where(b => b._Category.CategoryName.ToLower() == Cat_name.ToLower()).ToListAsync();

                if (catP1 == null)
                {
                    return null;
                }

                return _mapper.Map<List<ProductWithCategoryDto>>(catP1);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteProduct(int id)
        {
            try
            {
                var isExists = await _context.Products.FirstOrDefaultAsync(a => a.Id == id);
                if (isExists == null)
                {
                    return false;
                }

                _context.Products.Remove(isExists);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<ProductWithCategoryDto>> SearchProduct(string search)
        {
            try
            {
                if (string.IsNullOrEmpty(search))
                {
                    return new List<ProductWithCategoryDto> { new ProductWithCategoryDto() };
                }

                var pro = await _context.Products.Include(x=>x._Category).Where(a => a.ProductName.ToLower().Contains(search.ToLower())).ToListAsync();

                return _mapper.Map<List<ProductWithCategoryDto>>(pro);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdatePro(int id, AddProductDto addpro, IFormFile image)
        {
            try
            {
                var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
                if (product == null)
                    throw new Exception("Product not found");

                string imageUrl = product.ImageUrl;
                if (image != null)
                {
                    imageUrl = await _cloudinary.UploadImageAsync(image);
                }

                var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == addpro.CategoryId);
                if (category == null)
                    throw new Exception("Invalid Category ID");

                product.ProductName = addpro.ProductName;
                product.ProductDescription = addpro.ProductDescription;
                product.ProductPrize = addpro.ProductPrice;
                product.offerPrice = addpro.OfferPrize;
                product.Rating = addpro.Rating;
                product.ImageUrl = imageUrl;
                product.CategoryId = category.Id;

                _context.Products.Update(product);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
