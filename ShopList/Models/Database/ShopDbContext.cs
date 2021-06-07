using Microsoft.EntityFrameworkCore;
using ShopList.Models.Database.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ShopList.Models.Database
{
    public class ShopDbContext : 
        IdentityDbContext<UserEntity, RoleEntity, int, IdentityUserClaim<int>, UserRoleEntity,
        IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options)
        {
        }

        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<CartEntity> Cart { get; set; }
        public DbSet<ReviewEntity> Review { get; set; }
        public DbSet<PurchaseHistoryEntity> PurchaseHistory { get; set; }
        public DbSet<CategoryEntity> Category { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserEntity>()
                .HasMany(e => e.UserRoles)
                .WithOne(e => e.User)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();

            modelBuilder.Entity<RoleEntity>()
                .HasMany(e => e.UserRoles)
                .WithOne(e => e.Role)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();

            //modelBuilder.Entity<ProductEntity>()
            //    .HasMany(cp => cp.CartProducts)
            //    .WithOne(cp => cp.Product)
            //    .HasForeignKey(p => p.Id)
            //    .IsRequired();

            //modelBuilder.Entity<CartEntity>()
            //    .HasMany(cp => cp.CartProducts)
            //    .WithOne(cp => cp.Cart)
            //    .HasForeignKey(c => c.Id)
            //    .IsRequired();


            //modelBuilder.Entity<CategoryEntity>()
            //    .HasMany(cp => cp.CategoryProducts)
            //    .WithOne(cp => cp.Category)
            //    .HasForeignKey(c => c.Id)
            //    .IsRequired();

            //modelBuilder.Entity<ProductEntity>()
            //    .HasMany(cp => cp.CartProducts)
            //    .WithOne(p => p.Product)
            //    .HasForeignKey(p => p.Id)
            //    .IsRequired();
        }
    }
}
