
using BackendProject2.CloudinaryServices;
using E_Commerce.CloudinaryServices;
using E_Commerce.Context;
using E_Commerce.CustomMiddleweare;
using E_Commerce.Mapper;
using E_Commerce.Service;
using E_Commerce.Service.Address;
using E_Commerce.Service.Cart;
using E_Commerce.Service.CategoryService;
using E_Commerce.Service.CategoryServices;
using E_Commerce.Service.Order;
using E_Commerce.Service.ProductService;
using E_Commerce.Service.WishList;
using E_Commerce.Service.WishListServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace E_Commerce
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<AppDbContext>(Options =>
         Options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IAuthServices,AuthServices>();
            builder.Services.AddAutoMapper(typeof(ProfileMapper));
            builder.Services.AddScoped<ICategoryServices,CategoryServices>();
            builder.Services.AddScoped<ICloudinaryServices,CloudinaryService>();
            builder.Services.AddScoped<IProductServices,ProductServices>();
            builder.Services.AddScoped<ICartService, CartService>();
            builder.Services.AddScoped<IWishListServices,WishListServices>();
            builder.Services.AddScoped<IOrderServices,OrderServices>();
            builder.Services.AddScoped<IAddressServices,AddressServices>();

            // Swagger Configuration
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' followed by a space and your JWT token."
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });





            // JWT Authentication Configuration 
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"])),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true
                };
                o.RequireHttpsMetadata = false;  // Use true in production for security
                o.SaveToken = true;
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseMiddleware<UserIdMiddleware>();
            app.MapControllers();

            app.Run();
        }
    }
}
