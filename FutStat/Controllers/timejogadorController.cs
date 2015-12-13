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
    public class timejogadorController : Controller
    {
        private futebolEntities db = new futebolEntities();

        // GET: timejogador
        public ActionResult Index()
        {
            var time_jogador = db.time_jogador.Include(t => t.jogador).Include(t => t.time);
            return View(time_jogador.ToList());
        }

        // GET: timejogador/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            time_jogador time_jogador = db.time_jogador.Find(id);
            if (time_jogador == null)
            {
                return HttpNotFound();
            }
            return View(time_jogador);
        }

        // GET: timejogador/Create
        public ActionResult Create()
        {
            ViewBag.CD_JOGADOR = new SelectList(db.jogador, "CD_JOGADOR", "DS_NOME");
            ViewBag.CD_TIME = new SelectList(db.time, "CD_TIME", "DS_NOME");
            return View();
        }

        // POST: timejogador/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CD_TIME,CD_JOGADOR,DT_CHEGADA,DT_SAIDA")] time_jogador time_jogador)
        {
            if (ModelState.IsValid)
            {
                db.time_jogador.Add(time_jogador);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CD_JOGADOR = new SelectList(db.jogador, "CD_JOGADOR", "DS_NOME", time_jogador.CD_JOGADOR);
            ViewBag.CD_TIME = new SelectList(db.time, "CD_TIME", "DS_NOME", time_jogador.CD_TIME);
            return View(time_jogador);
        }

        // GET: timejogador/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            time_jogador time_jogador = db.time_jogador.Find(id);
            if (time_jogador == null)
            {
                return HttpNotFound();
            }
            ViewBag.CD_JOGADOR = new SelectList(db.jogador, "CD_JOGADOR", "DS_NOME", time_jogador.CD_JOGADOR);
            ViewBag.CD_TIME = new SelectList(db.time, "CD_TIME", "DS_NOME", time_jogador.CD_TIME);
            return View(time_jogador);
        }

        // POST: timejogador/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CD_TIME,CD_JOGADOR,DT_CHEGADA,DT_SAIDA")] time_jogador time_jogador)
        {
            if (ModelState.IsValid)
            {
                db.Entry(time_jogador).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CD_JOGADOR = new SelectList(db.jogador, "CD_JOGADOR", "DS_NOME", time_jogador.CD_JOGADOR);
            ViewBag.CD_TIME = new SelectList(db.time, "CD_TIME", "DS_NOME", time_jogador.CD_TIME);
            return View(time_jogador);
        }

        // GET: timejogador/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            time_jogador time_jogador = db.time_jogador.Find(id);
            if (time_jogador == null)
            {
                return HttpNotFound();
            }
            return View(time_jogador);
        }

        // POST: timejogador/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            time_jogador time_jogador = db.time_jogador.Find(id);
            db.time_jogador.Remove(time_jogador);
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
