using System.Collections.Generic;

namespace Shoplister.Models
{
  public class Store
  {
    public Store()
    {
      this.JoinEntities = new HashSet<ItemStore>();
    }

    public int StoreId { get; set; }
    public string StoreName { get; set; }
    public virtual ApplicationUser User { get; set; }
    public virtual ICollection<ItemStore> JoinEntities { get; set; }
  }
}