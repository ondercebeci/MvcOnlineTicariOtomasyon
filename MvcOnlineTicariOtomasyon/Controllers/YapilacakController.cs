using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class YapilacakController : Controller
    {
        // GET: Yapilacak
        Context c= new Context();
        public ActionResult Index()
        {
            var deger1 = c.Carilers.Count().ToString();
            ViewBag.v1=deger1;
            var deger2 = c.Uruns.Count().ToString();
            ViewBag.v2=deger2;
            var deger3=c.Kategoris.Count().ToString();
            ViewBag.v3=deger3;
            var deger4= (from x in c.Carilers select x.CariSehir).Distinct().Count().ToString();
            ViewBag.v4=deger4;
            var values = c.Yapilacaks.ToList();
            return View(values);
        }
    }
}