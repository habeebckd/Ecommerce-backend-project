using AutoMapper;
using E_Commerce.Dto.Address;
using E_Commerce.Dto.Cart;
using E_Commerce.Dto.category;
using E_Commerce.Dto.product;
using E_Commerce.Dto.user;
using E_Commerce.Dto.WishList;
using E_Commerce.Model;

namespace E_Commerce.Mapper
{
    public class ProfileMapper : Profile
    {
        public ProfileMapper() 
        {
            CreateMap<User,UserRegistrationDto>().ReverseMap();
            CreateMap<User,UserLoginDto>().ReverseMap();
            CreateMap<User,UserListDto>().ReverseMap();
            CreateMap<Category,CategoryDto>().ReverseMap();
            CreateMap<Category,CatAddDto>().ReverseMap();
            CreateMap<Product, ProductWithCategoryDto>()
 .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src._Category.CategoryName))
            .ReverseMap();
            CreateMap<Product,AddProductDto>().ReverseMap();
            CreateMap<CartItems, CartViewDto>().ReverseMap();          
            CreateMap<UserAddress,AddNewAddressDto>().ReverseMap();
            CreateMap<UserAddress, GetAddressDto>().ReverseMap();
        }
    }
}
