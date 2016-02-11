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

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public PartialViewResult _EntertainmentMiniPartialView()
        {
            return PartialView();
        }

        public PartialViewResult _PerformerMiniPartialView()
        {
            return PartialView();
        }

    }
}