using BasketApplication.Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BasketApplication.DataAccess
{
    public class ApplicationDBContext : IdentityDbContext
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions)
            : base(dbContextOptions)
        {
            Database.Migrate();
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<AppUserBasket> AppUserBaskets { get; set; }
        public DbSet<PurchaseHistory> PurchaseHistories { get; set; }
        public DbSet<PurchaseDetails> PurchaseDetails { get; set; }
        public DbSet<ProductStock> ProductStocks { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<PurchaseHistory>().HasKey(x => x.PurchaseId);

            builder.Entity<PurchaseDetails>().HasKey(x => new { x.PurchaseId, x.ProductId });

            builder.Entity<AppUserBasket>(x => x.HasKey(p => new { p.AppUserId, p.ProductId }));

            builder.Entity<AppUserBasket>()
                .HasOne(u => u.AppUser)
                .WithMany(u => u.AppUserBasket)
                .HasForeignKey(u => u.AppUserId);

            builder.Entity<AppUserBasket>()
                .HasOne(u => u.Product)
                .WithMany(u => u.AppUserBaskets)
                .HasForeignKey(u => u.ProductId);

            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName= "ADMIN"
                },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "User"
                }
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
