using MvcOnlineTicariOtomasyon.Models.Siniflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class CariController : Controller
    {
        // GET: Cari
        Context c = new Context();
        public ActionResult Index()
        {
            var values = c.Carilers.Where(x => x.Durum == true).ToList();
            return View(values);
        }
        [HttpGet]
        public ActionResult YeniCari()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniCari(Cariler p)
        {
            p.Durum = true;
            c.Carilers.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult CariSil(int id)
        {
            var values = c.Carilers.Find(id);
            values.Durum = false;
            c.SaveChanges();

            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult CariGetir(int id)
        {
            var values = c.Carilers.Find(id);
            return View("CariGetir", values);
        }
        [HttpPost]
        public ActionResult CariGüncelle(Cariler p)
        {
            if (!ModelState.IsValid)

            {
                return View("CariGetir");
            }
            var values = c.Carilers.Find(p.Cariid);
            values.CariAd = p.CariAd;
            values.CariSoyad = p.CariSoyad;
            values.CariSehir = p.CariSehir;
            values.CariMail = p.CariMail;
            c.SaveChanges();
            return RedirectToAction("Index");

        }
        public ActionResult MusteriSatis(int id)
        {
            var values = c.SatisHarekets.Where(x => x.Cariid == id).ToList();
            var cr = c.Carilers.Where(x => x.Cariid == id).Select(y => y.CariAd + " " + y.CariSoyad).FirstOrDefault();
            ViewBag.cari = cr;
            return View(values);
        }
        

    }
}