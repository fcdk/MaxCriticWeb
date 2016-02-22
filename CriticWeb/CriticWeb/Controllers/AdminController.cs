using CriticWeb.DataLayer;
using CriticWeb.Models;
using CriticWeb.Models.AdminViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Web;
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

        [HttpPost]
        public void ChangeRole(Guid id, string role)
        {
            UserCritic userFromMySchema = UserCritic.GetById(id);

            ApplicationUserManager userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser user = userManager.FindByEmail(userFromMySchema.Email);

            IList<string> roles = userManager.GetRoles(user.Id);
            foreach (var rol in roles)
            {
                userManager.RemoveFromRole(user.Id, rol);
            }
            userManager.AddToRole(user.Id, role);

            UserCritic.Role userRole = (UserCritic.Role)Enum.Parse(typeof(UserCritic.Role), role);

            userFromMySchema.UserRole = userRole;
            userFromMySchema.Save();
        }

    }
}