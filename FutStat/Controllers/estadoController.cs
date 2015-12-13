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
    public class estadoController : Controller
    {
        private futebolEntities db = new futebolEntities();

        public ActionResult Index()
        {
            return View(db.estado.ToList().OrderBy(e => e.DS_ESTADO));
        }

        public ActionResult IndexJSON()
        {
            var estados = db.estado.ToList().OrderBy(e => e.DS_ESTADO);
            var est = (from e in estados
                       select new
                       {
                           CD_ESTADO = e.CD_ESTADO,
                           DS_ESTADO = e.DS_ESTADO,
                           cidades = (from c in e.cidade
                                      select new
                                      {
                                          CD_CIDADE = c.CD_CIDADE,
                                          DS_CIDADE = c.DS_CIDADE
                                      })
                       });
            return Json(est, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(int? id)
        {
            estado estado;
            if (id.HasValue)
                estado = db.estado.Find(id);
            else
                estado = new estado();

            return View(estado);
        }

        [HttpPost]
        public ActionResult Edit(estado estado)
        {
            if (ModelState.IsValid)
            {
                if (estado.CD_ESTADO != 0)
                    db.Entry(estado).State = EntityState.Modified;
                else
                    db.estado.Add(estado);

                db.SaveChanges();
                return Json(estado, JsonRequestBehavior.AllowGet);
            }
            return View(estado);
        }

        [HttpPost]
        public ActionResult Delete(estado estado)
        {
            if (estado != null)
            {
                estado = db.estado.Find(estado.CD_ESTADO);
                db.estado.Remove(estado);
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
