using System;
using System.Collections.Generic;
using AmazonClone.Areas.Admin.Models;
using Microsoft.EntityFrameworkCore;

namespace AmazonClone.Areas.Admin.Data;

public partial class Amazon3Context : DbContext
{
    public Amazon3Context()
    {
    }

    public Amazon3Context(DbContextOptions<Amazon3Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<DeliveryOption> DeliveryOptions { get; set; }

    public virtual DbSet<Login> Logins { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Tracking> Trackings { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=ACER\\MSSQLSERVER03;Initial Catalog=Amazon3;Persist Security Info=True;User ID=amazonweb;Password=12345;Encrypt=True;Trust Server Certificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.CartId).HasName("PK__Cart__415B03D81C2EF6D4");

            entity.ToTable("Cart");

            entity.Property(e => e.CartId)
                .HasMaxLength(50)
                .HasColumnName("cartID");
            entity.Property(e => e.DeliveryOptionId)
                .HasMaxLength(50)
                .HasColumnName("deliveryOptionID");
            entity.Property(e => e.ProductId)
                .HasMaxLength(50)
                .HasColumnName("productID");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.UserId)
                .HasMaxLength(50)
                .HasColumnName("userID");

            entity.HasOne(d => d.DeliveryOption).WithMany(p => p.Carts)
                .HasForeignKey(d => d.DeliveryOptionId)
                .HasConstraintName("FK__Cart__deliveryOp__4316F928");

            entity.HasOne(d => d.Product).WithMany(p => p.Carts)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__Cart__productID__4222D4EF");

            entity.HasOne(d => d.User).WithMany(p => p.Carts)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Cart__userID__412EB0B6");
        });

        modelBuilder.Entity<DeliveryOption>(entity =>
        {
            entity.HasKey(e => e.DeliveryOptionId).HasName("PK__Delivery__5B6D8C907D963CAF");

            entity.ToTable("DeliveryOption");

            entity.Property(e => e.DeliveryOptionId)
                .HasMaxLength(50)
                .HasColumnName("deliveryOptionID");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price");
        });

        modelBuilder.Entity<Login>(entity =>
        {
            entity.HasKey(e => e.LoginId).HasName("PK__Login__1F5EF42F6E4047DB");

            entity.ToTable("Login");

            entity.Property(e => e.LoginId)
                .HasMaxLength(50)
                .HasColumnName("loginID");
            entity.Property(e => e.LoginTime)
                .HasColumnType("datetime")
                .HasColumnName("loginTime");
            entity.Property(e => e.LogoutTime)
                .HasColumnType("datetime")
                .HasColumnName("logoutTime");
            entity.Property(e => e.UserId)
                .HasMaxLength(50)
                .HasColumnName("userID");

            entity.HasOne(d => d.User).WithMany(p => p.Logins)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Login__userID__3A81B327");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Order__0809337DB6A98B73");

            entity.ToTable("Order");

            entity.Property(e => e.OrderId)
                .HasMaxLength(50)
                .HasColumnName("orderID");
            entity.Property(e => e.DeliveryDate)
                .HasColumnType("datetime")
                .HasColumnName("deliveryDate");
            entity.Property(e => e.OrderDate)
                .HasColumnType("datetime")
                .HasColumnName("orderDate");
            entity.Property(e => e.ProductId)
                .HasMaxLength(50)
                .HasColumnName("productID");
            entity.Property(e => e.TotalPrice)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("totalPrice");
            entity.Property(e => e.UserId)
                .HasMaxLength(50)
                .HasColumnName("userID");

            entity.HasOne(d => d.Product).WithMany(p => p.Orders)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__Order__productID__46E78A0C");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Order__deliveryD__45F365D3");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Products__2D10D14A5B04A7EE");

            entity.Property(e => e.ProductId)
                .HasMaxLength(50)
                .HasColumnName("productID");
            entity.Property(e => e.Counting)
                .HasMaxLength(100)
                .HasColumnName("counting");
            entity.Property(e => e.Image)
                .HasMaxLength(255)
                .HasColumnName("image");
            entity.Property(e => e.InstructionsLink)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("instructions_link");
            entity.Property(e => e.Keywords)
                .HasMaxLength(100)
                .HasColumnName("keywords");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.PriceCents)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("priceCents");
            entity.Property(e => e.SizeChartLink)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("size_chart_link");
            entity.Property(e => e.Stars)
                .HasMaxLength(100)
                .HasColumnName("stars");
            entity.Property(e => e.Type)
                .HasMaxLength(100)
                .HasColumnName("type");
            entity.Property(e => e.WarrantyLink)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("warranty_link");
        });

        modelBuilder.Entity<Tracking>(entity =>
        {
            entity.HasKey(e => e.TrackingId).HasName("PK__Tracking__A815748E9A9D5C6C");

            entity.ToTable("Tracking");

            entity.Property(e => e.TrackingId)
                .HasMaxLength(50)
                .HasColumnName("trackingID");
            entity.Property(e => e.OrderId)
                .HasMaxLength(50)
                .HasColumnName("orderID");
            entity.Property(e => e.ProductId)
                .HasMaxLength(50)
                .HasColumnName("productID");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
            entity.Property(e => e.UpdateTime)
                .HasColumnType("datetime")
                .HasColumnName("updateTime");

            entity.HasOne(d => d.Order).WithMany(p => p.Trackings)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__Tracking__orderI__49C3F6B7");

            entity.HasOne(d => d.Product).WithMany(p => p.Trackings)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__Tracking__produc__4AB81AF0");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__CB9A1CDF062C8C06");

            entity.HasIndex(e => e.Email, "UQ__Users__AB6E6164FE136F5C").IsUnique();

            entity.Property(e => e.UserId)
                .HasMaxLength(50)
                .HasColumnName("userID");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .HasColumnName("password");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
