using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YonetimPaneli.Models;

namespace YonetimPaneli.Controllers
{
    public class AdminController : Controller
    {
        // Veritabanı nesnesi
        KurumsalDBEntities1 db = new KurumsalDBEntities1();

        // Admin Paneli Ana Sayfa
        public ActionResult Index()
        {
            // Kategorileri listeleyip view'a gönderiyoruz
            var sorgu = db.Kategori.ToList();
            return View(sorgu);
        }

        // GET: Admin/Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();  // Boş login formunu göstermek için
        }

        // POST: Admin/Login
        [HttpPost]
        public ActionResult Login(Admin admin)
        {
            // Giriş yapmaya çalışan kullanıcıyı sorguluyoruz
            var login = db.Admin.SingleOrDefault(x => x.Eposta == admin.Eposta);

            if (login != null && login.Sifre == admin.Sifre)
            {
                // Giriş başarılı olduğunda session verilerini kaydediyoruz
                Session["adminId"] = login.AdminId;
                Session["eposta"] = login.Eposta;
                return RedirectToAction("Index", "Admin");  // Admin paneline yönlendirme
            }

            // Eğer giriş başarısızsa uyarı mesajı gösteriliyor
            ViewBag.Uyari = "Kullanıcı adı veya şifre yanlış";
            return View(admin);  // Aynı sayfayı geri döndürüp uyarıyı gösteriyoruz
        }
        public ActionResult Logout()
        {
            Session["adminId"] = null;
            Session["eposta"] = null;
            Session.Abandon();
            return RedirectToAction("Login", "Admin");
        
        }
    }
}
