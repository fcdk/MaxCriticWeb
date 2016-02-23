using CriticWeb.DataLayer;
using CriticWeb.Models.ContentCriticViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CriticWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            MainPageViewModel mainPage = new MainPageViewModel();            
            return View(mainPage);
        }

        public PartialViewResult _EntertainmentMiniPartialView()
        {
            return PartialView();
        }

        public PartialViewResult _PerformerMiniPartialView()
        {
            return PartialView();
        }

        public ActionResult EntertainmentDetails(Guid id)
        {
            return View(new EntertainmentDetailsViewModel(id));
        }

        public PartialViewResult _ReviewPartialView()
        {
            return PartialView();
        }

        public ActionResult PerformerDetails(Guid id)
        {
            return View(new PerformerDetailsViewModel(id));
        }

        public PartialViewResult _PerformerAndEntertainmentPartialView()
        {
            return PartialView();
        }

        [HttpPost]
        public void RateForContent(int vote, Guid id)
        {
            Review review = new Review(ProfileCritic.Instance.CurrentUserCritic, Entertainment.GetById(id), (byte)vote, "", DateTime.UtcNow, null, ProfileCritic.Instance.CurrentUserCritic.UserRole == UserCritic.Role.Critic ? ProfileCritic.Instance.CurrentUserCritic.PublicationCompany : null, 0, 0, ProfileCritic.Instance.CurrentUserCritic.UserRole == UserCritic.Role.Critic ? true : false);
            review.Save();
        }

        [HttpPost]
        public void OpinionAndLinkCaption(string opinion, string link, Guid id)
        {
            Review review = Review.GetReviewByEntertainmentAndUser(Entertainment.GetById(id), ProfileCritic.Instance.CurrentUserCritic);
            review.Opinion = opinion;
            if (link != "undefined")
            {
                review.Link = link;
            }
            review.Save();
        }

        [HttpPost]
        public void RateForReview(bool helpful, Guid id)
        {
            Review review = Review.GetById(id);
            if (helpful)
                review.Helpful += 1;
            else review.Unhelpful += 1;
            review.Save();
        }

        public PartialViewResult _AllReviewsPartialView()
        {
            return PartialView();
        }

        public ActionResult EntertainmentReviews(Guid id, bool isCritic)
        {
            return View(new EntertainmentReviewsViewModel(id, isCritic));
        }

        public ActionResult UserProfile(Guid id)
        {
            return View(new UserProfileViewModel(id));
        }

        public PartialViewResult _AllEntertainmentsAndPerformersPartialView()
        {
            return PartialView();
        }

    }
}