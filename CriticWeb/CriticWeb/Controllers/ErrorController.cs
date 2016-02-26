using System.Web.Mvc;

namespace CriticWeb.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Error()
        {
            return View();
        }

        public ActionResult PageNotFound()
        {
            return View();
        }
    }
}