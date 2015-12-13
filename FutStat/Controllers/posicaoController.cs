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
    public class posicaoController : Controller
    {
        private futebolEntities db = new futebolEntities();

        // GET: posicao
        public ActionResult Index()
        {
            return View(db.posicao.ToList());
        }

        public ActionResult IndexJSON()
        {
            return Json(db.posicao.ToList().OrderBy(p => p.DS_POSICAO), JsonRequestBehavior.AllowGet);
        }

        // GET: posicao/Edit/5
        public ActionResult Edit(int? id)
        {
            posicao posicao;
            if (id.HasValue)
                posicao = db.posicao.Find(id);
            else
                posicao = new posicao();

            return View(posicao);
        }

        [HttpPost]
        public ActionResult Edit(posicao posicao)
        {
            if (ModelState.IsValid)
            {
                if (posicao.CD_POSICAO != 0)
                    db.Entry(posicao).State = EntityState.Modified;
                else
                    db.posicao.Add(posicao);

                db.SaveChanges();
                return Json(posicao, JsonRequestBehavior.AllowGet);
            }
            return View(posicao);
        }

        [HttpPost]
        public ActionResult Delete(posicao posicao)
        {
            if (posicao != null)
            {
                posicao = db.posicao.Find(posicao.CD_POSICAO);
                db.posicao.Remove(posicao);
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
