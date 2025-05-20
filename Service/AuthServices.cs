using AutoMapper;
using E_Commerce.Context;
using E_Commerce.Dto.user;
using E_Commerce.Model;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace E_Commerce.Service
{
    public class AuthServices : IAuthServices
    {

        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        public AuthServices(AppDbContext context, IConfiguration configuration, IMapper mapper)
        {
            _context = context;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<bool> Register(UserRegistrationDto userRegistrationDto)
        {
            try
            {
                userRegistrationDto.UserName=userRegistrationDto.UserName.Trim();
                userRegistrationDto.UserEmail=userRegistrationDto.UserEmail.Trim();
                userRegistrationDto.Passoword=userRegistrationDto.Passoword.Trim();


                var IsExists = await _context.Users.FirstOrDefaultAsync(a => a.UserEmail == userRegistrationDto.UserEmail);
                if (IsExists != null)
                {
                    return false;
                }


                var HashPassword = BCrypt.Net.BCrypt.HashPassword(userRegistrationDto.Passoword);


                var res = _mapper.Map<User>(userRegistrationDto);
                res.Passoword = HashPassword;
                _context.Users.Add(res);
                await _context.SaveChangesAsync();

                return true;

            }
            catch (DbUpdateException dbex)
            {
                throw new Exception($"Database error: {dbex.InnerException?.Message ?? dbex.Message}");
            }
        }


        public async Task<UserResponseDto> Login(UserLoginDto userLogin_dto)
        {
            try
            {
                userLogin_dto.UserEmail = userLogin_dto.UserEmail.Trim();
                userLogin_dto.Passoword = userLogin_dto.Passoword?.Trim();
                

                var u = await _context.Users.FirstOrDefaultAsync(a => a.UserEmail == userLogin_dto.UserEmail);
                if (u == null)
                {
                    return new UserResponseDto { Error = "Not Found" };
                }

                var pass = validatePassword(userLogin_dto.Passoword, u.Passoword);
                if (!pass)
                {
                    return new UserResponseDto { Error = "Invalid Password" };
                }

                if (u.isBlocked == true)
                {
                    return new UserResponseDto { Error = "User Blocked" };
                }
                var Token = Generate_Token(u);
                return new UserResponseDto
                {
                    UserName = u.UserName,
                    Token = Token,
                    UserEmail = u.UserEmail,
                    Role = u.Role,
                    Id = u.Id
                };

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<UserListDto>> AllUsers()
        {
            var users = await _context.Users.ToListAsync();
            return _mapper.Map<List<UserListDto>>(users);
        }
        private string Generate_Token(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));
            var credentails = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claim = new[]
            {
                new Claim(ClaimTypes.NameIdentifier ,user.Id.ToString()),
                new Claim(ClaimTypes.Name,user.UserName ),
                new Claim(ClaimTypes.Role,user.Role),
                new Claim(ClaimTypes.Email,user.UserEmail)
            };

            var token = new JwtSecurityToken(
                claims: claim,
                signingCredentials: credentails,
                expires: DateTime.UtcNow.AddDays(1)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }



        private bool validatePassword(string password, string hashpassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashpassword);
        }




    }
}
