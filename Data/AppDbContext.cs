using Microsoft.EntityFrameworkCore;
using Project2.Models;

namespace Project2.Data;

public class AppDbContext : DbContext
{
  public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
  {
  }

  public DbSet<Product> Products { get; set; }
}