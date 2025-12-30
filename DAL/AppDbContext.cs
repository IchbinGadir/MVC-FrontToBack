using Microsoft.EntityFrameworkCore;
using ProniaA.Models;
using ProniaA.Models.Base;

namespace ProniaA.DAL;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions optionsBuilder) : base(optionsBuilder) { }



    public DbSet<Slide> Slides { get; set; }
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<ProductImage> ProductImages { get; set; }



}












