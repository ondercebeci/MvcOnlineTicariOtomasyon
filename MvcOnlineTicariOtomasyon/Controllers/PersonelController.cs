using MvcOnlineTicariOtomasyon.Models.Siniflar;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows.Markup;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class PersonelController : Controller
    {
        // GET: Personel
        Context c = new Context();
        public ActionResult Index()
        {
            var varlues = c.Personels.ToList();
            return View(varlues);
        }

        [HttpGet]
        public ActionResult PersonelEkle()
        {
            List<SelectListItem> deger1 = (from x in c.Departmans.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.DepartmanAd,
                                               Value = x.Departmanid.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;
            return View();
        }
        [HttpPost]
        public ActionResult PersonelEkle(Personel p)
        {
            if (Request.Files.Count > 0)
            {
                string dosyaadi = Path.GetFileName(Request.Files[0].FileName);
                string uzanti = Path.GetExtension(Request.Files[0].FileName);
                string yol = "~/Image/" + dosyaadi ;
                Request.Files[0].SaveAs(Server.MapPath(yol));
                p.PersonelGorsel = "~/Image/" + dosyaadi;
            }
            c.Personels.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult PersonelGetir(int id)
        {
            List<SelectListItem> deger1 = (from x in c.Departmans.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.DepartmanAd,
                                               Value = x.Departmanid.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;
            var values = c.Personels.Find(id);
            return View("PersonelGetir", values);
        }
        public ActionResult PersonelGüncelle(Personel p)
        {

            if (Request.Files.Count > 0)
            {
                string dosyaadi = Path.GetFileName(Request.Files[0].FileName);
                string uzanti = Path.GetExtension(Request.Files[0].FileName);
                string yol = "~/Image/" + dosyaadi;
                Request.Files[0].SaveAs(Server.MapPath(yol));
                p.PersonelGorsel = "~/Image/" + dosyaadi;
            }
            var values = c.Personels.Find(p.Personelid);

            values.PersonelAd = p.PersonelAd;
            values.PersonelSoyad = p.PersonelSoyad;
            values.PersonelGorsel = p.PersonelGorsel;
            values.Departmanid = p.Departmanid;
            c.SaveChanges();
            return RedirectToAction("Index");

        }
        public ActionResult PersonelListe()
        {
            var values = c.Personels.ToList();
            return View(values);
        }
    }
}