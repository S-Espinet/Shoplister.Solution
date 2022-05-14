using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Shoplister.Models
{
  public class ShoplisterContext : IdentityDbContext<ApplicationUser>
  {
    public DbSet<Store> Stores { get; set; }
    public DbSet<Item> Items { get; set; }

    public DbSet<ItemStore> ItemStore { get; set; }
    public ShoplisterContext(DbContextOptions options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseLazyLoadingProxies();
    }
  }
}