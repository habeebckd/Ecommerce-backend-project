using E_Commerce.Model;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext>options): base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Category>Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItems> CartItems { get; set; }
        public DbSet<WishList> wishList { get; set; }
        public DbSet<UserAddress> userAddress {  get; set; }
        public DbSet<Order> order { get; set; }
        public DbSet<OrderItems> orderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .Property(x => x.Role)
                .HasDefaultValue("User");

            modelBuilder.Entity<User>()
                .Property(x => x.isBlocked)
                .HasDefaultValue(false);

            modelBuilder.Entity<Product>()
                .Property(pr => pr.ProductPrize)
                .HasPrecision(18, 2);                                                                                                               

            modelBuilder.Entity<Product>()
                .Property(pr=> pr.offerPrice)
                .HasPrecision(18,2);

            modelBuilder.Entity<Product>()
                .Property(pr=>pr.Rating)
                .HasPrecision(3,1);

            modelBuilder.Entity<Product>()
                .Property(pr => pr.StockId)
                .HasDefaultValue(50);

            modelBuilder.Entity<Product>()
                .HasOne(a => a._Category)
                .WithMany(b => b._Products)
                .HasForeignKey(c => c.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CartItems>()
                .Property(a => a.ProductQty)
                .HasDefaultValue(1);

            modelBuilder.Entity<Cart>()
                .HasOne(a => a._User)
                .WithOne(b => b._Cart)
                .HasForeignKey<Cart>(c => c.UserId);

            modelBuilder.Entity<CartItems>()
                .HasOne(a=>a._Cart)
                .WithMany(b=>b._Items)
                .HasForeignKey(c=>c.CartId);

            modelBuilder.Entity<CartItems>()
                .HasOne(a => a._Product)
                .WithMany(b => b._CartItems)
                .HasForeignKey(c => c.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<WishList>()
                .HasOne(a => a._User)
                .WithMany(b => b._WishLists)
                .HasForeignKey(c => c.UserId);

            modelBuilder.Entity<WishList>()
                .HasOne(a=>a._Product)
                .WithMany()
                .HasForeignKey(c=>c.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Order>()
               .HasOne(a => a._User)
               .WithMany(b => b._Orders)
               .HasForeignKey(c => c.UserId)
               .OnDelete(DeleteBehavior.Restrict); // 🔥 Fix to prevent multiple cascade paths

            modelBuilder.Entity<Order>()
                .HasOne(a => a._UserAd)
                .WithMany(b => b._Orders)
                .HasForeignKey(c => c.AddressId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Order>()
                .Property(pr => pr.OrderStatus)
                .HasDefaultValue("OderPlaced");

            modelBuilder.Entity<Order>()
                .Property(pr => pr.Total)
                .HasPrecision(30, 2);

            modelBuilder.Entity<OrderItems>()
                .HasOne(a => a._Order)
                .WithMany(b => b._Items)
                .HasForeignKey(c => c.OrderId);

            modelBuilder.Entity<OrderItems>()
                .HasOne(a => a.Product)
                .WithMany()
                .HasForeignKey(c => c.ProductId);

            modelBuilder.Entity<OrderItems>()
                .Property(pr => pr.TotalPrice)
                .HasPrecision(18, 2);


            modelBuilder.Entity<UserAddress>()
                .HasOne(a => a._UserAd)
                .WithMany(b => b._UserAddresses)
                .HasForeignKey(c => c.UserId);

        }
    }
}

