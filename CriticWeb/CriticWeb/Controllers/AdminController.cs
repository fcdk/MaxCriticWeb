using CriticWeb.DataLayer;
using CriticWeb.Models.AdminViewModels;
using System;
using System.Web.Mvc;

namespace CriticWeb.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        public ActionResult ReviewsChecking()
        {
            return View(new ReviewsChekingViewModel());
        }

        [HttpPost]
        public void ConfirmReview(Guid id)
        {
            Review review = Review.GetById(id);
            review.CheckedByAdmin = true;
            review.Save();
        }

        [HttpDelete]
        public void AbortReview(Guid id)
        {
            Review review = Review.GetById(id);
            review.Delete();
        }

        public ActionResult UsersAdministrating(string username = null)
        {
            return View(new UsersAdministratingViewModel(username));
        }

    }
}