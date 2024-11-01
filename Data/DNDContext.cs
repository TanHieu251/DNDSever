using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using DNDServer.Authen.Request;
using DNDServer.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DNDServer.Data;

public partial class DNDContext : IdentityDbContext<ApplicationUser>
{
    public DNDContext(DbContextOptions<DNDContext> options)
        : base(options)
    {

    }
    public DbSet<TypeProduct> TypeProducts { get; set; }
    public DbSet<TypeProject> TypeProjects { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Project> Projects { get; set; }

    public DbSet<ImgProduct> ImgProducts { get; set; }
    public DbSet<ImgProject> ImgProjects { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // PROJECT
        builder.Entity<Project>()
           .HasOne(p => p.TypeProject)
            .WithMany(tp => tp.Project)
            .HasForeignKey(p => p.TypeData);

        // IMG PRODUCT
        builder.Entity<ImgProject>()
            .HasOne(p => p.Projects)
                .WithMany(tp => tp.ImgProjects)
                .HasForeignKey(p => p.ProjectId);
            
    // ORDER
        builder.Entity<Order>()
            .HasOne(p => p.User)
                .WithMany(tp => tp.Orders)
                .HasForeignKey(p => p.UserId);
        // ORDER DETAIL
        builder.Entity<OrderDetails>()
            .HasKey(k => new { k.OrderId, k.ProductId });

        builder.Entity<OrderDetails>()
            .HasOne(p => p.Order)
            .WithMany(tp => tp.OrderDetail)
            .HasForeignKey(p => p.OrderId);

        builder.Entity<OrderDetails>()
            .HasOne(p => p.Product)
            .WithMany(tp => tp.OrderDetails)
            .HasForeignKey(p => p.ProductId);


        //TYPE PRODUCT
        builder.Entity<TypeProduct>()
            .HasKey(k => k.Id);

        builder.Entity<TypeProduct>()
           .Property(p => p.Id)
           .ValueGeneratedOnAdd();

        //TYPE PRODJECT
        builder.Entity<TypeProject>()
            .HasKey(k => k.Id);

        builder.Entity<TypeProject>()
           .Property(p => p.Id)
           .ValueGeneratedOnAdd();

        // PRODUCT
        builder.Entity<Product>()
            .HasKey(k => k.Id);

        builder.Entity<Product>()
           .Property(p => p.Id)
           .ValueGeneratedOnAdd();

        // PROJECT
        builder.Entity<Project>()
            .HasKey(k => k.Id);

        builder.Entity<Project>()
           .Property(p => p.Id)
           .ValueGeneratedOnAdd();



    }

public DbSet<DNDServer.Model.Project> Project { get; set; } = default!;

}
