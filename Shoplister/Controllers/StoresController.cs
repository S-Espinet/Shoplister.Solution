using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
  public class StoresController : Controller
  {
    private readonly ShoplisterContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public StoresController(UserManager<ApplicationUser> userManager, ShoplisterContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    public async Task<ActionResult> Index()
    {
      ViewBag.PageTitle = "View All Stores";
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      var userStores = _db.Stores.Where(entry => entry.User.Id == currentUser.Id).ToList();
      return View(userStores.OrderBy(store => store.StoreName).ToList());
    }
    
    
    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Create(Store store)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      store.User = currentUser;

      var dbStoreMatch =_db.Stores.FirstOrDefault(dbStore => (dbStore.StoreName == store.StoreName) && (dbStore.User == store.User));
      if(dbStoreMatch == null)
      {
        _db.Stores.Add(store);
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    
    public ActionResult Edit(int id)
    {
      Console.WriteLine(id);
      var thisStore = _db.Stores.FirstOrDefault(store => store.StoreId == id);
      Console.WriteLine(thisStore.StoreName);
      return View(thisStore);
    }

    [HttpPost]
    public ActionResult Edit (Store store)
    {
      _db.Entry(store).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }


    public ActionResult Delete(int id)
    {
      var thisStore = _db.Stores.FirstOrDefault(dbStore => dbStore.StoreId == id);
      _db.Stores.Remove(thisStore);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }


    public ActionResult Details(int id)
    {
      var thisStore = _db.Stores
        .Include(store => store.JoinEntities)
        .FirstOrDefault(store => store.StoreId == id);
      return View(thisStore.JoinEntities.ToList());
    }

    [HttpPost]
    public ActionResult Details(List<ItemStore> storesItems)
    {
      foreach(var newItem in storesItems)
      {
        var dbItemMatch =_db.Items.FirstOrDefault(dbItem => dbItem.ItemId == newItem.ItemId);
        if(dbItemMatch != null)
        {
          dbItemMatch.Checked = newItem.Item.Checked; 
        }    
      }
      _db.SaveChanges();
      return RedirectToAction("Details");
    }
  }
}