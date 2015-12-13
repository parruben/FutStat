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
    public class cidadeController : Controller
    {
        private futebolEntities db = new futebolEntities();

        // GET: cidade
        public ActionResult Index()
        {
            var cidade = db.cidade.Include(c => c.estado);
            return View(cidade.ToList());
        }

        public ActionResult IndexJSON()
        {
            var cidades = db.cidade.Include(c => c.estado).OrderBy(c => c.estado.DS_ESTADO).ThenBy(c => c.DS_CIDADE);
            var cid = (from c in cidades
                       select new
                       {
                           CD_CIDADE = c.CD_CIDADE,
                           DS_CIDADE = c.DS_CIDADE,
                           CD_ESTADO = c.CD_ESTADO,
                           estado = new
                           {
                               CD_ESTADO = c.estado.CD_ESTADO,
                               DS_ESTADO = c.estado.DS_ESTADO
                           }
                       });
            return Json(cid, JsonRequestBehavior.AllowGet);
        }

        // GET: cidade/Edit/5
        public ActionResult Edit(int? id)
        {
            cidade cidade;
            if (id.HasValue)
                cidade = db.cidade.Find(id);
            else
                cidade = new cidade();

            ViewBag.CD_ESTADO = new SelectList(db.estado.OrderBy(e => e.DS_ESTADO), "CD_ESTADO", "DS_ESTADO", cidade.CD_ESTADO);
            return View(cidade);
        }

        [HttpPost]
        public ActionResult Edit(cidade cidade)
        {
            if (ModelState.IsValid)
            {
                if (cidade.CD_CIDADE != 0)
                    db.Entry(cidade).State = EntityState.Modified;
                else
                    db.cidade.Add(cidade);

                db.SaveChanges();
                cidade.estado = db.estado.Find(cidade.CD_ESTADO);

                var cid = new
                {
                    CD_CIDADE = cidade.CD_CIDADE,
                    DS_CIDADE = cidade.DS_CIDADE,
                    CD_ESTADO = cidade.CD_ESTADO,
                    estado = new
                    {
                        CD_ESTADO = cidade.estado.CD_ESTADO,
                        DS_ESTADO = cidade.estado.DS_ESTADO
                    }
                };

                return Json(cid, JsonRequestBehavior.AllowGet);
            }
            ViewBag.CD_ESTADO = new SelectList(db.estado, "CD_ESTADO", "DS_ESTADO", cidade.CD_ESTADO);
            return View(cidade);
        }

        // POST: cidade/Delete/5
        [HttpPost]
        public ActionResult Delete(cidade cidade)
        {
            if (cidade != null)
            {
                cidade = db.cidade.Find(cidade.CD_CIDADE);
                db.cidade.Remove(cidade);
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
