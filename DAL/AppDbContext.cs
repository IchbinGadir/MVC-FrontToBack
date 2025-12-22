using Microsoft.EntityFrameworkCore;
using ProniaA.Models;

namespace ProniaA.DAL;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> optionsBuilder) : base(optionsBuilder)
    {


    }

    public DbSet<Slide> Slides { get; set; }
    public DbSet<Blog> Blogs { get; set; }

}

