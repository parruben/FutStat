using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FutStat.Models;

namespace FutStat.Controllers
{
    public class campeonatoController : Controller
    {
        private futebolEntities db = new futebolEntities();

        // GET: campeonato
        public ActionResult Index()
        {
            return View(db.campeonato.ToList());
        }

        public ActionResult IndexJSON()
        {
            return Json(db.campeonato.ToList().OrderBy(c => c.DS_NOME), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Edit(campeonato campeonato)
        {
            if (ModelState.IsValid)
            {
                if (campeonato.CD_CAMPEONATO != 0)
                    db.Entry(campeonato).State = EntityState.Modified;
                else
                    db.campeonato.Add(campeonato);

                db.SaveChanges();
                return Json(campeonato, JsonRequestBehavior.AllowGet);
            }
            return View(campeonato);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult Delete(campeonato campeonato)
        {
            campeonato = db.campeonato.Find(campeonato.CD_CAMPEONATO);
            db.campeonato.Remove(campeonato);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
