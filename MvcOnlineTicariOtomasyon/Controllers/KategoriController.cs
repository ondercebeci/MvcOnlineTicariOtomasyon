﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;
using PagedList;
using PagedList.Mvc;
namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class KategoriController : Controller
    {
        // GET: Kategori
        Context c = new Context();
        public ActionResult Index(int sayfa=1)
        {
            var degerler = c.Kategoris.ToList().ToPagedList(sayfa, 4);
            return View(degerler);
        }
        [HttpGet]
        public ActionResult KategoriEkle()
        {

            return View();
        }
        [HttpPost]
        public ActionResult KategoriEkle(Kategori k)
        {

            c.Kategoris.Add(k);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult KategoriSil(int id)
        {
            var ktg = c.Kategoris.Find(id);
            c.Kategoris.Remove(ktg);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult KategoriGüncelle(int id)
        {
            var kategori = c.Kategoris.Find(id);
            return View("KategoriGüncelle", kategori);
             
        }
        public ActionResult KategoriGüncelleme(Kategori k)
        {
            var ktg = c.Kategoris.Find(k.KategoriID);
            ktg.KategoriAd = k.KategoriAd;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Deneme()
        {
            Class3 cs = new Class3();
            cs.Kategoriler = new SelectList(c.Kategoris, "KategoriID", "KategoriAd");
            cs.Urunler = new SelectList(c.Uruns, "Urunid", "UrunAd");
            return View(cs);
        }
        public JsonResult UrunGetir(int p)
        {
            var urunListesi = (from x in c.Uruns
                               join y in c.Kategoris on
                               x.Kategori.KategoriID equals y.KategoriID
                               where x.Kategori.KategoriID == p
                               select new
                               {
                                   Text= x.UrunAd,
                                   Value=x.Urunid.ToString()
                               }).ToList();
            return Json(urunListesi,JsonRequestBehavior.AllowGet);
        }
    }
}