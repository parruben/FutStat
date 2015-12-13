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
    public class formacaoController : Controller
    {
        private futebolEntities db = new futebolEntities();

        // GET: formacao
        public ActionResult Index()
        {
            return View(db.formacao.ToList());
        }

        public ActionResult IndexJson()
        {
            return Json(db.formacao.ToList().OrderBy(f => f.DS_FORMACAO), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(int? id)
        {
            var formacao = new formacao();
            if (id.HasValue)
            {
                formacao = db.formacao.Find(id);
            }

            return View(formacao);
        }

        [HttpPost]
        public ActionResult Edit(formacao formacao)
        {
            if (ModelState.IsValid)
            {
                if (formacao.CD_FORMACAO != 0)
                    db.Entry(formacao).State = EntityState.Modified;
                else
                    db.formacao.Add(formacao);
                
                db.SaveChanges();
                return Json(formacao, JsonRequestBehavior.AllowGet);
            }
            return View(formacao);
        }

        [HttpPost]
        public ActionResult Delete(formacao formacao)
        {
            if (formacao != null)
            {
                formacao = db.formacao.Find(formacao.CD_FORMACAO);
                db.formacao.Remove(formacao);
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
