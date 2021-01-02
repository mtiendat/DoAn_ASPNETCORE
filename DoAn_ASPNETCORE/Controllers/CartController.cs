using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DoAn_ASPNETCORE.Areas.Admin.Data;
using DoAn_ASPNETCORE.Areas.Admin.Models;
using DoAn_ASPNETCORE.Heplers;

namespace DoAn_ASPNETCORE.Controllers
{
    [Route("Cart")]
    public class CartController : Controller
    {
        private readonly Webbanhang _context;

        public CartController(Webbanhang context)
        {
            _context = context;
        }
        [Route("index")]

        public IActionResult Index()
        {        
            var cart = SessionHelper.GetObjectFromJson<List<ItemModel>>(HttpContext.Session, "cart");
            ViewBag.cart = cart;
           
            ViewBag.total = cart.Sum(item => item.SanPham.Gia * item.QuanTity);
            return View();
        }


        [Route("buy/{id}")]
        public IActionResult Buy(int id)
        {

            if (SessionHelper.GetObjectFromJson<List<ItemModel>>(HttpContext.Session, "cart") == null)
            {
                List<ItemModel> cart = new List<ItemModel>();
                cart.Add(new ItemModel { SanPham = _context.SanPhamModel.Find(id), QuanTity = 1 });
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                List<ItemModel> cart = SessionHelper.GetObjectFromJson<List<ItemModel>>(HttpContext.Session, "cart");
                int index = isExist(id);
                if (index != -1)
                {
                    cart[index].QuanTity++;
                }
                else
                {
                    cart.Add(new ItemModel { SanPham = _context.SanPhamModel.Find(id), QuanTity = 1 });
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            return RedirectToAction("Index");
         
        }

        

        [Route("remove/{id}")]
        public IActionResult Remove(int id)
        {
            List<ItemModel> cart = SessionHelper.GetObjectFromJson<List<ItemModel>>(HttpContext.Session, "cart");
            int index = isExist(id);
            cart.RemoveAt(index);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("Index");
        }

        private int isExist(int id)
        {
            List<ItemModel> cart = SessionHelper.GetObjectFromJson<List<ItemModel>>(HttpContext.Session, "cart");
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].SanPham.ID.Equals(id))
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
