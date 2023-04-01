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
    public class LoaiMonsController : Controller
    {
        private FoodModelContext db = new FoodModelContext();

        // GET: Admin/LoaiMons
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View(db.LoaiMon.ToList());
        }

        // GET: Admin/LoaiMons/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiMon loaiMon = db.LoaiMon.Find(id);
            if (loaiMon == null)
            {
                return HttpNotFound();
            }
            return View(loaiMon);
        }

        // GET: Admin/LoaiMons/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/LoaiMons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaLoaiMon,TenLoaiMon")] LoaiMon loaiMon)
        {
            if (ModelState.IsValid)
            {
                db.LoaiMon.Add(loaiMon);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(loaiMon);
        }

        // GET: Admin/LoaiMons/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiMon loaiMon = db.LoaiMon.Find(id);
            if (loaiMon == null)
            {
                return HttpNotFound();
            }
            return View(loaiMon);
        }

        // POST: Admin/LoaiMons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaLoaiMon,TenLoaiMon")] LoaiMon loaiMon)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loaiMon).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(loaiMon);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiMon category = db.LoaiMon.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Admin/Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    LoaiMon category = db.LoaiMon.Find(id);
                    if (category == null)
                    {
                        return HttpNotFound();
                    }

                    var books = db.Mon.Where(b => b.MaLoaiMon == id).ToList();
                    foreach (var book in books)
                    {
                        db.Mon.Remove(book);
                    }

                    db.LoaiMon.Remove(category);
                    db.SaveChanges();
                    dbContextTransaction.Commit();
                }
                catch
                {
                    dbContextTransaction.Rollback();
                    // handle exception
                }
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
