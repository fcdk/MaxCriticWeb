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
        public void RateForContent(int vote)
        {

        }

    }
}