using CriticWeb.Models.AdminViewModels;
using CriticWeb.Models.ContentCriticViewModels;
using System.Web.Mvc;

namespace CriticWeb.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        public ActionResult ReviewsChecking()
        {
            ReviewsChekingViewModel reviewChecking = new ReviewsChekingViewModel();
            return View(reviewChecking);
        }
    }
}