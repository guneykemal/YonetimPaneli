using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using YonetimPaneli.Models;


namespace YonetimPaneli.Controllers
{
    public class KimlikController : Controller
    {
        // GET: Kimlik
        KurumsalDBEntities1 db = new KurumsalDBEntities1();

        public ActionResult Index()
        {
            return View(db.Kimlik.ToList());
        }

        // GET: Kimlik/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Kimlik/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Kimlik/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Kimlik/Edit/5
        public ActionResult Edit(int id)
        {
            var kimlik = db.Kimlik.Where(x => x.KimlikId == id).SingleOrDefault();
            return View(kimlik);
        }

        // POST: Kimlik/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Kimlik model, HttpPostedFileBase LogoUrl)
        {
            if (ModelState.IsValid)
            {
                var k = db.Kimlik.Where(x => x.KimlikId == id).SingleOrDefault();

                if (LogoUrl != null)
                {
                    // Eski logoyu silme işlemi
                    if (System.IO.File.Exists(Server.MapPath(k.LogoUrl)))
                    {
                        System.IO.File.Delete(Server.MapPath(k.LogoUrl));
                    }

                    // Yeni logo işlemleri
                    WebImage img = new WebImage(LogoUrl.InputStream);
                    FileInfo fileInfo = new FileInfo(LogoUrl.FileName);
                    string logoname = fileInfo.Name;  // Doğru değişken ismi burada kullanıldı
                    img.Resize(100, 100);
                    img.Save(Server.MapPath("~/Uploads/Kimlik/" + logoname));  // Doğru dosya yolunu kullanın
                    k.LogoUrl = "/Uploads/Kimlik/" + logoname;
                }
                // `k` nesnesinin güncellenmesi
                k.Title = model.Title;
                k.Keywords = model.Keywords;
                k.Description = model.Description;
                k.Unvan = model.Unvan;

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }

    }
}
