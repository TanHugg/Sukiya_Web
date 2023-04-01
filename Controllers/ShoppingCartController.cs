using Microsoft.AspNet.Identity;
using Sukiya.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sukiya.Controllers
{
    public class ShoppingCartController : Controller
    {
        // GET: ShoppingCart
        public List<CartItem> GetShoppingCartFromSession()
        {
            var lstShoppingCart = Session["ShoppingCart"] as List<CartItem>;
            if (lstShoppingCart == null)
            {
                lstShoppingCart = new List<CartItem>();
                Session["ShoppingCart"] = lstShoppingCart;
            }
            return lstShoppingCart;
        }
        [Authorize]
        public RedirectToRouteResult AddToCart(int id)
        {
            FoodModelContext db = new FoodModelContext();
            List<CartItem> ShoppingCart = GetShoppingCartFromSession();
            CartItem findCartItem = ShoppingCart.FirstOrDefault(m => m.MaMon == id);
            if (findCartItem == null)
            {
                Mon findBook = db.Mon.First(m => m.MaMon == id);
                CartItem newItem = new CartItem()
                {
                    MaMon = findBook.MaMon,
                    TenMon = findBook.TenMon,
                    SoLuong = 1,
                    HinhAnh = findBook.HinhAnh,
                    Gia = findBook.Gia.Value
                };
                ShoppingCart.Add(newItem);
            }
            else
                findCartItem.SoLuong++;
            return RedirectToAction("Index", "ShoppingCart");
        }
        public ActionResult Index()
        {
            List<CartItem> ShoppingCart = GetShoppingCartFromSession();
            if (ShoppingCart.Count == 0)
            {
                return RedirectToAction("Index", "Mons");

            }
            ViewBag.TongsoLuong = ShoppingCart.Sum(p => p.SoLuong);
            ViewBag.TongTien = ShoppingCart.Sum(p => p.SoLuong * p.Gia);
            return View(ShoppingCart);
        }

        public RedirectToRouteResult UpdateCart(int id, int txtQuantity)
        {
            var itemFind = GetShoppingCartFromSession().FirstOrDefault(m => m.MaMon == id);
            if (itemFind != null)
            {
                itemFind.SoLuong = txtQuantity;
            }
            return RedirectToAction("Index");
        }
        public ActionResult CartSummary()
        {
            ViewBag.CartCount = GetShoppingCartFromSession().Count();
            return PartialView("CartSummary");
        }
        public ActionResult Order()
        {
            int id = -1;
            string currentUserId = User.Identity.GetUserId();
            FoodModelContext context = new FoodModelContext();
            using (DbContextTransaction transaction = context.Database.BeginTransaction())
            {
                try
                {
                    DonHang objOrder = new DonHang()
                    {
                        CustomerId = currentUserId,
                        NgayDat = DateTime.Now,
                        NgayGiao = null,
                        DaGiao = false,
                        DaThanhToan = false

                    };
                    objOrder = context.DonHang.Add(objOrder);
                    context.SaveChanges();
                    id = objOrder.MaDonHang;
                    List<CartItem> listCartItem = GetShoppingCartFromSession();
                    foreach (var item in listCartItem)
                    {
                        CT_DonHang ctdh = new CT_DonHang()
                        {
                            MaDonHang = objOrder.MaDonHang,
                            MaMon = item.MaMon,
                            SoLuong = item.SoLuong,
                            Gia = item.Gia,
                        };
                        context.CT_DonHang.Add(ctdh);
                        context.SaveChanges();
                    }
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return Content("Loi khi dat hang.." + ex.Message);
                }
            }
            Session["Giohang"] = null;
            return RedirectToAction("Payment", "Home", new { id = id });
        }
        public ActionResult ConfirmOrder()
        {
            return View();
        }
        public ActionResult RemoveCartItem(int id)
        {
            List<CartItem> ShoppingCart = GetShoppingCartFromSession();
            var itemFind = GetShoppingCartFromSession().FirstOrDefault(m => m.MaMon == id);
            if (itemFind != null)
            {
                ShoppingCart.Remove(itemFind);
            }
            return RedirectToAction("Index", "ShoppingCart");
        }
        public ActionResult RemoveCart()
        {
            List<CartItem> ShoppingCart = GetShoppingCartFromSession();
            ShoppingCart.Clear();
            return RedirectToAction("Index", "Mons");
        }

    }
}