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
    public class jogadorController : Controller
    {
        private futebolEntities db = new futebolEntities();

        // GET: jogador
        public ActionResult Index()
        {
            var jogador = db.jogador.Include(j => j.posicao);
            return View(jogador.ToList());
        }

        // GET: jogador/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            jogador jogador = db.jogador.Find(id);
            if (jogador == null)
            {
                return HttpNotFound();
            }
            return View(jogador);
        }

        // GET: jogador/Create
        public ActionResult Create()
        {
            ViewBag.CD_POSICAO = new SelectList(db.posicao.OrderBy(p => p.DS_POSICAO), "CD_POSICAO", "DS_POSICAO");
            return View();
        }

        // POST: jogador/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CD_JOGADOR,CD_POSICAO,DS_NOME,DS_SOBRENOME,DS_APELIDO,DT_NASCIMENTO")] jogador jogador)
        {
            if (ModelState.IsValid)
            {
                db.jogador.Add(jogador);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CD_POSICAO = new SelectList(db.posicao, "CD_POSICAO", "DS_POSICAO", jogador.CD_POSICAO);
            return View(jogador);
        }

        // GET: jogador/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            jogador jogador = db.jogador.Find(id);
            if (jogador == null)
            {
                return HttpNotFound();
            }
            ViewBag.CD_POSICAO = new SelectList(db.posicao, "CD_POSICAO", "DS_POSICAO", jogador.CD_POSICAO);
            return View(jogador);
        }

        // POST: jogador/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CD_JOGADOR,CD_POSICAO,DS_NOME,DS_SOBRENOME,DS_APELIDO,DT_NASCIMENTO")] jogador jogador)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jogador).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CD_POSICAO = new SelectList(db.posicao, "CD_POSICAO", "DS_POSICAO", jogador.CD_POSICAO);
            return View(jogador);
        }

        // GET: jogador/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            jogador jogador = db.jogador.Find(id);
            if (jogador == null)
            {
                return HttpNotFound();
            }
            return View(jogador);
        }

        // POST: jogador/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            jogador jogador = db.jogador.Find(id);
            db.jogador.Remove(jogador);
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
