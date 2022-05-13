using System.Collections.Generic;

namespace Shoplister.Models
{
  public class Store
  {
    public Store()
    {
      this.Items = new HashSet<Item>();
    }

    public int StoreId { get; set; }
    public string StoreName { get; set; }
    public virtual ICollection<Item> Items { get; set; }
  }
}