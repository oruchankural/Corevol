using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Corevol.Models;
using Microsoft.AspNetCore.Mvc;

namespace Corevol.Controllers
{
    public class DefaultController : Controller
    {
        Context c = new Context();
        public IActionResult Index()
        {
            var dep = c.Birims.ToList();
            return View(dep);
        }

        [HttpGet]
        public IActionResult YeniBirim()
        {
            return View();
        }
        [HttpPost]
        public IActionResult YeniBirim(Birim p)
        {
            c.Birims.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index");

        }
        public IActionResult BirimSil(int id)
        {
            var dep = c.Birims.Find(id);
            c.Birims.Remove(dep);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult BirimGetir(int id)
        {
            var birim = c.Birims.Find(id);
            return View("BirimGetir", birim);
        }

        public IActionResult BirimGuncelle(Birim d)
        {
            var birimler = c.Birims.Find(d.BirimID);
            birimler.BirimAd = d.BirimAd;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult BirimDetay(int id)
        {
            var detay = c.Personels.Where(x => x.BirimID == id).ToList();
            var birimad = c.Birims.Where(x => x.BirimID == id).Select(y => y.BirimAd).FirstOrDefault();
            ViewBag.birim = birimad;
            return View(detay);
        }
    }
}