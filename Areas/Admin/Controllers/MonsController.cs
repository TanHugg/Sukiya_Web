using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Sukiya.Models;

namespace Sukiya.Areas.Admin.Controllers
{
    public class MonsController : Controller
    {
        private FoodModelContext db = new FoodModelContext();

        // GET: Admin/Mons
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var mon = db.Mon.Include(m => m.LoaiMon);
            return View(mon.ToList());
        }

        public ActionResult Index1()
        {
            var mon = db.Mon.Include(m => m.LoaiMon);
            return View(mon.ToList());
        }
        // GET: Admin/Mons/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mon mon = db.Mon.Find(id);
            if (mon == null)
            {
                return HttpNotFound();
            }
            return View(mon);
        }

        // GET: Admin/Mons/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.MaLoaiMon = new SelectList(db.LoaiMon, "MaLoaiMon", "TenLoaiMon");
            return View();
        }

        // POST: Admin/Mons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaMon,TenMon,MaLoaiMon,Gia,MoTa,HinhAnh")] Mon mon)
        {
            if (ModelState.IsValid)
            {
                db.Mon.Add(mon);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaLoaiMon = new SelectList(db.LoaiMon, "MaLoaiMon", "TenLoaiMon", mon.MaLoaiMon);
            return View(mon);
        }

        // GET: Admin/Mons/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mon mon = db.Mon.Find(id);
            if (mon == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaLoaiMon = new SelectList(db.LoaiMon, "MaLoaiMon", "TenLoaiMon", mon.MaLoaiMon);
            return View(mon);
        }

        // POST: Admin/Mons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaMon,TenMon,MaLoaiMon,Gia,MoTa,HinhAnh")] Mon mon)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mon).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaLoaiMon = new SelectList(db.LoaiMon, "MaLoaiMon", "TenLoaiMon", mon.MaLoaiMon);
            return View(mon);
        }

        // GET: Admin/Mons/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mon mon = db.Mon.Find(id);
            if (mon == null)
            {
                return HttpNotFound();
            }
            return View(mon);
        }

        // POST: Admin/Mons/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Mon mon = db.Mon.Find(id);
            db.Mon.Remove(mon);
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
