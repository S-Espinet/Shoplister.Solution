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
      return View(userItems.OrderBy(item => item.ItemName));
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
    public async Task<ActionResult> Create(Item item)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      item.User = currentUser;

      if (_db.Items.Where(dbItem => dbItem.ItemName == item.ItemName && dbItem.Checked == true).Any() == true) {
        //set checked to false
        _db.SaveChanges();
      }
      else
      {
        _db.Items.Add(item);
        _db.SaveChanges();
      }
      return RedirectToAction("Index");
    }
  }
}