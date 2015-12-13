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
    public class timeController : Controller
    {
        private futebolEntities db = new futebolEntities();

        // GET: time
        public ActionResult Index()
        {
            return View(db.time.ToList());
        }

        public ActionResult IndexJSON()
        {
            var times = db.time.ToList().OrderBy(t => t.DS_NOME);
            var tim = (from t in times
                       select new
                       {
                           CD_TIME = t.CD_TIME,
                           DS_NOME = t.DS_NOME,
                           DT_FUNDACAO = t.DT_FUNDACAO,
                           DT_FUNDACAO_VW = t.DT_FUNDACAO,
                           DT_FUNDACAO_ED = t.DT_FUNDACAO,
                           DS_IMG_ESCUDO = t.DS_IMG_ESCUDO,
                           DS_ESTADIO = t.DS_ESTADIO,
                           CD_CIDADE = t.CD_CIDADE,
                           cidade = new
                           {
                               CD_CIDADE = t.cidade.CD_CIDADE,
                               DS_CIDADE = t.cidade.DS_CIDADE
                           }
                       });
            return Json(tim, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Edit(time time)
        {
            if (ModelState.IsValid)
            {
                if (time.CD_TIME != 0)
                    db.Entry(time).State = EntityState.Modified;
                else
                    db.time.Add(time);

                db.SaveChanges();
                time.cidade = db.cidade.Find(time.CD_CIDADE);

                var tim = new
                {
                    CD_TIME = time.CD_TIME,
                    DS_NOME = time.DS_NOME,
                    DT_FUNDACAO = time.DT_FUNDACAO,
                    DT_FUNDACAO_VW = time.DT_FUNDACAO,
                    DT_FUNDACAO_ED = time.DT_FUNDACAO,
                    DS_IMG_ESCUDO = time.DS_IMG_ESCUDO,
                    DS_ESTADIO = time.DS_ESTADIO,
                    CD_CIDADE = time.CD_CIDADE,
                    cidade = new
                    {
                        CD_CIDADE = time.cidade.CD_CIDADE,
                        DS_CIDADE = time.cidade.DS_CIDADE
                    }
                };
                return Json(tim, JsonRequestBehavior.AllowGet);
            }
            return Json(time, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Delete(time time)
        {
            if (time != null)
            {
                time = db.time.Find(time.CD_TIME);
                db.time.Remove(time);
                db.SaveChanges();
            }
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
