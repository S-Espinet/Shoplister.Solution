using System.Collections.Generic;
using System;

namespace Shoplister.Models
{
  public class Item
  {
    public Item()
    {
      this.JoinEntities = new HashSet<ItemStore>();
    }
    public int ItemId { get; set; }
    public string ItemName { get; set; }
    public string Notes { get; set; }
    public bool Checked { get; set; } = false;
    public virtual ApplicationUser User { get; set; }
    public virtual ICollection<ItemStore> JoinEntities { get; }
  }
}