using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using YonetimPaneli.Models;
using System.Data.Entity;


namespace YonetimPaneli.Controllers
{
    public class HizmetController : Controller
    {
        // GET: Hizmet
        private KurumsalDBEntities1 db = new KurumsalDBEntities1();

        public ActionResult Index()
        {
            return View(db.Hizmet.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Hizmet hizmet, HttpPostedFileBase ResimUrl)
        {
            if (ModelState.IsValid)
            {
                if (ResimUrl != null)
                {
                    
                    WebImage img = new WebImage(ResimUrl.InputStream);
                    FileInfo imginfo = new FileInfo(ResimUrl.FileName); 

                    string ResimName = imginfo.Name + imginfo.Extension; 
                    img.Resize(300, 300);
                    img.Save("~/Uploads/Hizmet/"+ ResimName); 

                    hizmet.ResimUrl = "/Uploads/Hizmet/"+ ResimName; 
                }

                
                db.Hizmet.Add(hizmet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }

        public ActionResult Edit(int? id)
        {
            if(id == null)
            {
                ViewBag.Uyari = "Güncellenecek hizmet bulunamadı";

            }
            var hizmet = db.Hizmet.Find(id);
            if(hizmet == null)
            {

                return HttpNotFound();
            }


            return View(hizmet);


        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(int? id, Hizmet hizmet, HttpPostedFileBase ResimUrl)
        {
            var h = db.Hizmet.Where(x => x.HizmetId == id).FirstOrDefault(); 
            if (ModelState.IsValid)
            {
                if (ResimUrl != null)
                {
                    
                    if (System.IO.File.Exists(Server.MapPath(h.ResimUrl)))
                    {
                        System.IO.File.Delete(Server.MapPath(h.ResimUrl));
                    }

                    
                    WebImage img = new WebImage(ResimUrl.InputStream);
                    FileInfo imginfo = new FileInfo(ResimUrl.FileName);
                    string HizmetResimName = imginfo.Name + imginfo.Extension;
                    img.Resize(300, 300);
                    img.Save("~/Uploads/Hizmet/" + HizmetResimName);
                    h.ResimUrl = "/Uploads/Hizmet/" + HizmetResimName; 
                }

               
                h.Baslik = hizmet.Baslik;
                h.Aciklama = hizmet.Aciklama;


                h.Baslik=hizmet.Baslik;
                h.Aciklama =hizmet.Aciklama;


                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(h);
        }
        public ActionResult Delete( int id)
        {
            if(id == null)
            {
                return HttpNotFound();
            }
            var h = db.Hizmet.Find(id);
            if(h == null)
            {
                return HttpNotFound();
            }
            db.Hizmet.Remove(h);
            db.SaveChanges();
            return RedirectToAction("Index");
            
        }

    }
}
