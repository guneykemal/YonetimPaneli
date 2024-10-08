using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YonetimPaneli.Models;

namespace YonetimPaneli.Controllers
{
    public class HakimizdaController : Controller
    {
        // GET: Hakimizda
        KurumsalDBEntities1 db = new KurumsalDBEntities1();

        public ActionResult Index()
        {
            var h = db.Hakkimizdaa.ToList();
            return View(h);
        }

        public ActionResult Edit(int id)
        {
            var h = db.Hakkimizdaa.FirstOrDefault(x => x.HakkimizdaId == id);
           
            return View(h);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id,Hakkimizdaa h)
        {
            if (ModelState.IsValid)
            {
                var Hakkimizdaa = db.Hakkimizdaa.Where(x => x.HakkimizdaId == id).SingleOrDefault();
                Hakkimizdaa.Aciklama = h.Aciklama;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

           
            return View(h);

        }
    }
}
