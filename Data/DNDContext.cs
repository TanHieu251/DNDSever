using System;
using System.Collections.Generic;
using DNDServer.Authen.Request;
using DNDServer.DTO.Request;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DNDServer.Data;

public partial class DNDContext : IdentityDbContext<ApplicationUser>
{
    public DNDContext(DbContextOptions<DNDContext> options)
        : base(options)
    {

    }

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

        //category
        builder.Entity<TypeProduct>()
        .HasOne(p => p.Category)
        .WithMany(tp => tp.TypeProducts)
        .HasForeignKey(p => p.CategoryID);

    }

}
