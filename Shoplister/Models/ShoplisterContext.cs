//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Shoplister.Models
{
  public class ShoplisterContext : DbContext
  {
    public DbSet<Store> Stores { get; set; }
    public DbSet<Item> Items { get; set; }

    public ShoplisterContext(DbContextOptions options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseLazyLoadingProxies();
    }
  }
}