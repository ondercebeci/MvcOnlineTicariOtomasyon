using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class FaturaController : Controller
    {
        // GET: Fatura
        Context c = new Context();
        public ActionResult Index()
        {
            var values = c.Faturalars.ToList();
            return View(values);
        }
        [HttpGet]
        public ActionResult FaturaEkle()
        {

            return View();
        }
        [HttpPost]
        public ActionResult FaturaEkle(Faturalar p)
        {
            c.Faturalars.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult FaturaGetir(int id)
        {
            var values = c.Faturalars.Find(id);
            return View("FaturaGetir", values);
        }
        public ActionResult FaturaGuncelle(Faturalar p)
        {
            var values = c.Faturalars.Find(p.Faturaid);
            values.FaturaSeriNo = p.FaturaSeriNo;
            values.FaturaSıraNo = p.FaturaSıraNo;
            values.Saat = p.Saat;
            values.Tarih = p.Tarih;
            values.TeslimAlan = p.TeslimAlan;
            values.TeslimEden = p.TeslimEden;
            values.VergiDairesi = p.VergiDairesi;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult FaturaDetay(int id)
        {
            var values = c.FaturaKalems.Where(x => x.Faturaid == id).ToList();
            return View(values);
        }
        [HttpGet]
        public ActionResult YeniKalem()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniKalem(FaturaKalem p)
        {
            c.FaturaKalems.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Dinamik()
        {
            class4 cs = new class4();
            cs.deger1 = c.Faturalars.ToList();
            cs.deger2 = c.FaturaKalems.ToList();
            return View(cs);
        }
        public ActionResult FaturaKaydet(string FaturaSeriNo,string FaturaSıraNo, DateTime Tarih, string VergiDairesi,string Saat,string TeslimEden, string TeslimAlan, string Toplam, FaturaKalem[] kalemler)
        {
            Faturalar f = new Faturalar();
            f.FaturaSeriNo = FaturaSeriNo;
            f.FaturaSıraNo = FaturaSıraNo;
            f.Tarih = Tarih;
            f.VergiDairesi = VergiDairesi;
            f.Saat = Saat;
            f.TeslimEden = TeslimEden;
            f.TeslimAlan = TeslimAlan;
            f.Toplam = decimal.Parse(Toplam);
            c.Faturalars.Add(f);
            foreach(var item in kalemler)
            {
                FaturaKalem fk = new FaturaKalem();
                fk.Aciklama = item.Aciklama;
                fk.Miktar=item.Miktar;
                fk.BirimFiyat = item.BirimFiyat;
                fk.Tutar=item.Tutar;
                fk.FaturaKalemid = item.FaturaKalemid;
                c.FaturaKalems.Add(fk);
            }
            c.SaveChanges();
            return Json("İşlem Başarılı", JsonRequestBehavior.AllowGet);


        }
    }
}