using MvcOnlineTicariOtomasyon.Models.Siniflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcOnlineTicariOtomasyon.Controllers
{

    public class CariPanelController : Controller
    {
        Context c = new Context();
        [Authorize]
        // GET: CariPanel
        public ActionResult Index()
        {
            var mail = (string)Session["CariMail"];
            var values = c.mesajlars.Where(x => x.Alıcı == mail).ToList();
            ViewBag.v1 = mail;
            var mailid = c.Carilers.Where(x => x.CariMail == mail).Select(y => y.Cariid).FirstOrDefault();
            ViewBag.mailid = mailid;
            var toplamsatis = c.SatisHarekets.Count(x => x.Cariid == mailid);
            ViewBag.toplamsatis = toplamsatis;
            var toplamt = c.SatisHarekets.Where(x => x.Cariid == mailid).Sum(y => y.ToplamTutar);
            ViewBag.toplamt = toplamt;
            var toplamurunsayisi = c.SatisHarekets.Where(x => x.Cariid == mailid).Sum(y => y.Adet);
            ViewBag.toplamurunsayisi=toplamurunsayisi;
            var adsoyad= c.Carilers.Where(x=>x.CariMail== mail).Select(y => y.CariAd + " "+y.CariSoyad).FirstOrDefault();
            ViewBag.adsoyad = adsoyad;
            var sehir = c.Carilers.Where(x => x.CariMail == mail).Select(y => y.CariSehir).FirstOrDefault();
            ViewBag.sehir = sehir;
            return View(values);
        }
        [Authorize]
        public ActionResult Siparislerim()
        {
            var mail = (string)Session["CariMail"];
            var id = c.Carilers.Where(x => x.CariMail == mail).Select(y => y.Cariid).FirstOrDefault();
            var values = c.SatisHarekets.Where(x => x.Cariid == id).ToList();
            return View(values);
        }
        [Authorize]
        public ActionResult GelenMesajlar()
        {
            var mail = (string)Session["CariMail"];
            var mesajlar = c.mesajlars.Where(x => x.Alıcı == mail).OrderByDescending(x => x.Tarih).ToList();
            var gelensayi = c.mesajlars.Count(x => x.Alıcı == mail).ToString();
            ViewBag.v1 = gelensayi;
            var gidensayi = c.mesajlars.Count(x => x.Gonderici == mail).ToString();
            ViewBag.v2 = gidensayi;
            return View(mesajlar);
        }
        [Authorize]
        public ActionResult GidenMesajlar()
        {
            var mail = (string)Session["Carimail"];
            var mesajlar = c.mesajlars.Where(x => x.Gonderici == mail).OrderByDescending(x => x.Tarih).ToList();
            var gidensayi = c.mesajlars.Count(x => x.Gonderici == mail).ToString();
            ViewBag.v2 = gidensayi;
            var gelensayi = c.mesajlars.Count(x => x.Alıcı == mail).ToString();
            ViewBag.v1 = gelensayi;
            return View(mesajlar);
        }
        [Authorize]
        public ActionResult MesajDetay(int id)
        {
            var values = c.mesajlars.Where(x => x.MesajID == id).ToList();
            var mail = (string)Session["Carimail"];
            var gidensayi = c.mesajlars.Count(x => x.Gonderici == mail).ToString();
            ViewBag.v2 = gidensayi;
            var gelensayi = c.mesajlars.Count(x => x.Alıcı == mail).ToString();
            ViewBag.v1 = gelensayi;
            return View(values);
        }
        [Authorize]
        [HttpGet]
        public ActionResult YeniMesaj()
        {
            var mail = (string)Session["Carimail"];
            var gidensayi = c.mesajlars.Count(x => x.Gonderici == mail).ToString();
            ViewBag.v2 = gidensayi;
            var gelensayi = c.mesajlars.Count(x => x.Alıcı == mail).ToString();
            ViewBag.v1 = gelensayi;
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult YeniMesaj(mesajlar m)
        {
            var mail = (string)Session["Carimail"];
            m.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
            m.Gonderici = mail;

            c.mesajlars.Add(m);
            c.SaveChanges();
            return View();
        }
        [Authorize]
        public ActionResult KargoTakip(string p)
        {
            var kargot = from x in c.kargoDetays select x;

            kargot = kargot.Where(y => y.TakipKodu.Contains(p));

            return View(kargot.ToList());
        }
        [Authorize]
        public ActionResult CariKargoTakip(string id)
        {
            var values = c.kargoTakips.Where(x => x.TakipKodu == id);

            return View(values.ToList());
        }
        [Authorize]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
        [Authorize]
        public ActionResult Profil()
        {
            return View();
        }
        public PartialViewResult Partial1()
        {
            var mail = (string)Session["Carimail"];
            var id = c.Carilers.Where(x=>x.CariMail==mail).Select(y=>y.Cariid).FirstOrDefault();
            var caribul = c.Carilers.Find(id);
            return PartialView("Partial1",caribul);

        }
        public PartialViewResult Partial2()
        {
            var veriler = c.mesajlars.Where(x=>x.Gonderici=="Admin").ToList();
            return PartialView(veriler);
        }
        public  ActionResult CariBilgiGuncelle(Cariler cr)
        {
            var cari = c.Carilers.Find(cr.Cariid);
            cari.CariAd=cr.CariAd;
            cari.CariSoyad=cr.CariSoyad;
            cari.CariSifre=cr.CariSifre;
            c.SaveChanges();
            return RedirectToAction("Index");

        }


    }
}