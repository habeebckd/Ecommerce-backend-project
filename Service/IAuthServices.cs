using E_Commerce.Dto.user;
using E_Commerce.Model;

namespace E_Commerce.Service
{
    public interface IAuthServices
    {
        Task<bool>Register(UserRegistrationDto userRegistrationDto);
        Task<UserResponseDto>Login(UserLoginDto userLoginDto);
        
        Task<List<UserListDto>>AllUsers();

    }
}
