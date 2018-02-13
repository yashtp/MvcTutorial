using System.Collections.Generic;
using System.Web.Mvc;

namespace MvcTut.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public string Index(string id,string name)
        {
            return "Hello " + id + "Name" + Request.QueryString["name"];
        }

        public string GetDetails()
        {
            return "This is get details page";
        }

        public ActionResult MyList()
        {
            ViewBag.Countries = new List<string>
            {
                "India","China","America","Canada"
            };
            return View();
        }
    }
}