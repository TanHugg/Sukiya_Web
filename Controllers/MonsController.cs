using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Sukiya.Models;

namespace Sukiya.Controllers
{
    public class MonsController : Controller
    {
        private FoodModelContext db = new FoodModelContext();

        // GET: Mons
        public ActionResult Index(int? page)
        {

            int pageSize = 10;
            int pageIndex = page == null ? 1 : page.Value;
            var listProduct = db.Mon.Include(p => p.LoaiMon).ToList().ToPagedList(pageIndex, pageSize);
            return View(listProduct);
        }
        public ActionResult GetFoodByCategory(int id)
        {
            var context = new FoodModelContext();
            return View("Index", context.Mon.Where(p => p.MaLoaiMon == id).ToList().ToPagedList(1,4));
        }
        public ActionResult GetCategory()
        {
            var context = new FoodModelContext();
            var listCategory = context.LoaiMon.ToList();
            return PartialView(listCategory);
        }
        public ActionResult Details(int id)
        {
            var context = new FoodModelContext();
            var firstBook = context.Mon.FirstOrDefault(p => p.MaMon == id);
            if (firstBook == null)
                return HttpNotFound("Không tìm thấy mã sách này!");
            return View(firstBook);
        }
        public ActionResult Search(string searchstring)
        {
            var context = new FoodModelContext();
            var result = (from m in context.Mon where m.TenMon.Contains(searchstring) || m.MoTa.Contains(searchstring) select m);
            if (result.Count() > 0)
            {
                return View("Index", result.ToList().ToPagedList(1, 11));
            }
            return HttpNotFound("Khong co thong tin tim kiem");
        }
    }
}
