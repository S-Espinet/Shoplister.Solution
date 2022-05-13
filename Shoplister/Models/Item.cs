using System.Collections.Generic;
using System;

namespace Shoplister.Models
{
  public class Item
  {
    public int ItemId { get; set; }
    public string ItemName { get; set; }
    public string Notes { get; set; }
    public bool Checked { get; set; } = false;
    public int StoreId { get; set; }

    //public virtual ApplicationUser User { get; set; }

  }
}