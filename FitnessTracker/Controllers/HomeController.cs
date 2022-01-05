
using Microsoft.AspNetCore.Mvc;

namespace FitnessTracker.Controllers
{
  public class HomeController : Controller
  {
    [HttpGet("/")]
    public ActionResult Index()
    {
      return View();
    }
  }
}