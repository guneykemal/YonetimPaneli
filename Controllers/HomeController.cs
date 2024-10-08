using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YonetimPaneli.Models;

namespace YonetimPaneli.Controllers
{
    public class HomeController : Controller
    {
        private KurumsalDBEntities1 db= new KurumsalDBEntities1();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SliderPartial()
        {
            var sliderData = db.Slider.ToList(); // Slider tablosundan verileri çekin.
            return PartialView("SliderPartial", sliderData);
        }

    }
}