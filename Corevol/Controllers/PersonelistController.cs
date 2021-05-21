﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Corevol.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Corevol.Controllers
{
    public class PersonelistController : Controller
    {
        Context c = new Context();
        [Authorize]
        public IActionResult Index()
        {
            var value = c.Personels.Include(x=>x.Birim).ToList();
            return View(value);
        }

        [HttpGet]
        public IActionResult YeniPersonel()
        {
            List<SelectListItem> degerler = (from x in c.Birims.ToList()

                                             select new SelectListItem
                                             {
                                                 Text = x.BirimAd,
                                                 Value = x.BirimID.ToString()
                                             }
                                             ).ToList();
            ViewBag.dgr = degerler;
            return View();
        }
        [HttpPost]
        public IActionResult YeniPersonel(Personel p )
        {
            var per = c.Birims.Where(x => x.BirimID == p.Birim.BirimID).FirstOrDefault();
            p.Birim = per;
            c.Personels.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}