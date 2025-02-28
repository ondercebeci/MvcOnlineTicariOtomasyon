﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class UrunController : Controller
    {
        // GET: Urun
        Context c = new Context();
        public ActionResult Index(string p)
        {
            var urunler = from x in c.Uruns select x;
            if (!string.IsNullOrEmpty(p))
            {
                urunler = c.Uruns.Where(y => y.UrunAd.Contains(p));
            }
            return View(urunler.ToList());
        }
        [HttpGet]
        public ActionResult YeniUrun()
        {
            List<SelectListItem> deger1=(from x in c.Kategoris.ToList()
                                         select new SelectListItem
                                         {
                                             Text=x.KategoriAd,
                                             Value=x.KategoriID.ToString()
                                         }).ToList();
            ViewBag.Dgr1 = deger1;
            return View();
        }
        [HttpPost]
        public ActionResult YeniUrun(Urun p)
        {
           c.Uruns.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UrunSil(int id)
        {
            var deger= c.Uruns.Find(id);
            deger.Durum=false;
            c.SaveChanges();
            return RedirectToAction("Index");
            
        }
        public ActionResult UrunGetir(int id)
        {
            List<SelectListItem> deger1 = (from x in c.Kategoris.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.KategoriAd,
                                               Value = x.KategoriID.ToString()
                                           }).ToList();
            ViewBag.Dgr1 = deger1;
            var urundeger = c.Uruns.Find(id);
            return View("UrunGetir",urundeger);
        }
        public ActionResult UrunGuncelle(Urun p)
        { 
            var urn= c.Uruns.Find(p.Urunid);
            urn.AlisFiyat = p.AlisFiyat;
            urn.SatisFiyat=p.SatisFiyat;
            urn.Durum=p.Durum;
            urn.kategoriid=p.kategoriid;
            urn.Marka=p.Marka;
            urn.Stok=p.Stok;
            urn.UrunAd=p.UrunAd;
            urn.UrunGorsel=p.UrunGorsel;
            c.SaveChanges();
            return RedirectToAction("Index");
            
        }
        public ActionResult UrunListesi()
        {
            var degerler = c.Uruns.ToList();

            return View(degerler);
        }
        [HttpGet]
        public ActionResult SatisYap(int id)
        {
            List<SelectListItem> deger3 = (from x in c.Personels.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.PersonelAd + " " + x.PersonelSoyad,
                                               Value = x.Personelid.ToString()
                                           }).ToList();
            ViewBag.d3=deger3;
            var urundeger = c.Uruns.Find(id);
            ViewBag.d1 = urundeger.Urunid;
            ViewBag.d2=urundeger.SatisFiyat;
            return View(); 
        }
        [HttpPost]
        public ActionResult SatisYap(SatisHareket p)
        {
            p.Tarih=DateTime.Parse(DateTime.Now.ToShortDateString());
            c.SatisHarekets.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index","Satis");
        }
    }
}