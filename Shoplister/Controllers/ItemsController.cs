using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Shoplister.Models;
using System;

namespace Shoplister.Controllers
{
  [Authorize]
  public class ItemsController : Controller
  {
    private readonly ShoplisterContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public ItemsController(UserManager<ApplicationUser> userManager, ShoplisterContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    public async Task<ActionResult> Index()
    {
      ViewBag.PageTitle = "View All Items";
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      var userItems = _db.Items.Where(entry => entry.User.Id == currentUser.Id).ToList();
      return View(userItems.OrderBy(item => item.ItemName).ToList());
    }

    [HttpPost]
    public ActionResult Index (List<Item> myItems)
    {
      foreach(var newItem in myItems)
      {
        var dbItemMatch =_db.Items.FirstOrDefault(dbItem => dbItem.ItemId == newItem.ItemId);
        if(dbItemMatch != null)
        {
          dbItemMatch.Checked = newItem.Checked; 
        }    
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }


    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Create(Item item, int StoreId)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      item.User = currentUser;

      var dbItemMatch =_db.Items.FirstOrDefault(dbItem => (dbItem.ItemName == item.ItemName) && (dbItem.User == item.User));
      if(dbItemMatch != null)
      {
        dbItemMatch.Checked = false; 
      }
      else
      {
        _db.Items.Add(item);
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }


    public ActionResult Edit(int id)
    {
      var thisItem = _db.Items.FirstOrDefault(item => item.ItemId == id);
      return View(thisItem);
    }

    [HttpPost]
    public ActionResult Edit (Item item)
    {
      _db.Entry(item).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }


    public ActionResult Delete(int id)
    {
      var thisItem = _db.Items.FirstOrDefault(dbItem => dbItem.ItemId == id);
      _db.Items.Remove(thisItem);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }


    public ActionResult AddStore (int id)
    {
      var thisItem = _db.Items.FirstOrDefault(item => item.ItemId == id);
      ViewBag.StoreId = new SelectList(_db.Stores, "StoreId", "StoreName");
      return View(thisItem);
    }

    [HttpPost]
    public ActionResult AddStore (Item item, int StoreId)
    {
      if (StoreId != 0 && _db.ItemStore
          .Where(dbItemStore => dbItemStore.StoreId == StoreId && dbItemStore.ItemId == item.ItemId)
          .Any() == false)
      {
        _db.ItemStore.Add(new ItemStore() { StoreId = StoreId, ItemId = item.ItemId });
        _db.SaveChanges();
      }
      return RedirectToAction("Index");
    }


    [HttpPost]
    public ActionResult DeleteStore(int joinId)
    {
      var joinEntry = _db.ItemStore.FirstOrDefault(entry => entry.ItemStoreId == joinId);
      _db.ItemStore.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}