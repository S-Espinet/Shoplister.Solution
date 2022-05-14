namespace Shoplister.Models
{
  public class ItemStore
  {       
    public int ItemStoreId { get; set; }
    public int ItemId { get; set; }
    public int CategoryId { get; set; }
    public virtual Item Item { get; set; }
    public virtual Store Store { get; set; }
  }
}