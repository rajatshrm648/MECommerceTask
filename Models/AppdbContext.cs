using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MECommerceTask.Models
{

    public class Coupon
    {
        public int Id { get; set; }
        public string? Type { get; set; }
        public decimal DiscountValueInPercent { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
    public class CartWiseCoupon
    {
        public int Id { get; set; }
        [ForeignKey("Coupon")]
        public int CouponId { get; set; }
        public Coupon? Coupon { get; set; }
        public decimal Threshold { get; set; }
    }
    public class ProductWiseCoupon
    {
        public int Id { get; set; }
        [ForeignKey("Coupon")]
        public int CouponId { get; set; }
        public Coupon? Coupon { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product? Product { get; set; }
    }
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
    }

    public class Cart
    {
        public int Id { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal FinalPrice { get; set; }
        public List<CartItem>? Items { get; set; }
    }

    public class CartItem
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        public Cart? Cart { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<CartWiseCoupon> CartWiseCoupons { get; set; }
        public DbSet<ProductWiseCoupon> ProductWiseCoupons { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        // public DbSet<AppliedCoupon> AppliedCoupons { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CartWiseCoupon>()
                .HasOne(cwc => cwc.Coupon)
                .WithMany()
                .HasForeignKey(cwc => cwc.CouponId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProductWiseCoupon>()
            .HasOne(pwc => pwc.Coupon)
            .WithMany()
            .HasForeignKey(pwc => pwc.CouponId)
            .OnDelete(DeleteBehavior.Cascade); 

            modelBuilder.Entity<ProductWiseCoupon>()
                .HasOne(pwc => pwc.Product)
                .WithMany()
                .HasForeignKey(pwc => pwc.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }


}
