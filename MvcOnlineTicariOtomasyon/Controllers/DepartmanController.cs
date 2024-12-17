using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    [Authorize]
    public class DepartmanController : Controller
    {
        // GET: Departman
        Context c = new Context();
        
        public ActionResult Index()
        {
            var degerler = c.Departmans.Where(x => x.Durum == true).ToList();
            return View(degerler);
        }
        
        [HttpGet]
        public ActionResult DepartmanEkle()
        {
            return View();

        }

        [HttpPost]
        public ActionResult DepartmanEkle(Departman p)
        {
            c.Departmans.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DepartmanSil(int id)
        {
            var dep = c.Departmans.Find(id);
            dep.Durum = false;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DepartmanGetir(int id)
        {
            var dpt = c.Departmans.Find(id);
            return View("DepartmanGetir", dpt);

        }
        public ActionResult DepartmanGüncelle(Departman p)
        {
            var values = c.Departmans.Find(p.Departmanid);
            values.DepartmanAd = p.DepartmanAd;
            c.SaveChanges();
            return RedirectToAction("Index");

        }
        public ActionResult DepartmanDetay(int id)
        {
            var values = c.Personels.Where(x => x.Departmanid == id).ToList();
            var dpt= c.Departmans.Where(x=>x.Departmanid == id).Select(y=>y.DepartmanAd).FirstOrDefault();
            ViewBag.v1 = dpt;
            return View(values);
        }
        public ActionResult DepartmanPersonelSatis(int id)
        { 
            var values = c.SatisHarekets.Where(x=>x.Personelid == id).ToList();
            var pers= c.Personels.Where(x=>x.Personelid==id).Select(y=>y.PersonelAd + " "+ y.PersonelSoyad).FirstOrDefault();
            ViewBag.v2 = pers;
        return View(values);
        }
    }
}