using Microsoft.AspNetCore.Mvc;

namespace Shoplister.Controllers
{
    public class HomeController : Controller
    {

      [HttpGet("/")]
      public ActionResult Index()
      {
        ViewBag.PageTitle = "Shoplister";
        return View();
      }
    }
}
