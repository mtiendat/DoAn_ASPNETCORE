using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using DoAn_ASPNETCORE.Areas.Admin.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using DoAn_ASPNETCORE.Areas.Admin.Models;
using Newtonsoft.Json;

namespace DoAn_ASPNETCORE.Controllers
{

    public class PagesController : Controller
    {
        private readonly Webbanhang _context;

        public PagesController(Webbanhang context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {

            return View();
        }

        public async Task<IActionResult> products(int? id)
        {
            var iphone = (from m in _context.SanPhamModel
                          where m.MaLoai == id
                          select m).ToList();

            ViewBag.Loai = iphone;
            ViewBag.Username = HttpContext.Session.GetString("username");
            ViewBag.id = id;
            return View();
        }

        public async Task<IActionResult> detail(int? id)
        {

            var detail = (from m in _context.SanPhamModel
                          where m.ID == id
                          select m).ToList();

            ViewBag.ChiTiet = detail;
            ViewBag.Username = HttpContext.Session.GetString("username");
            return View();
        }
        public IActionResult Checkout()
        {
            ViewBag.Username = HttpContext.Session.GetString("username");
            return View();
        }
        public IActionResult Products1(int? id)
        {
            //var DsLaptop = (from m in _context.SanPhamModel
            //                where m.MaLoai == id
            //                select m).ToList();
            //ViewBag.LapTop = DsLaptop;
            ViewBag.id = id;
            ViewBag.Username = HttpContext.Session.GetString("username");
            return View();
        }
        //public IActionResult Login()
        //{
        //    ViewBag.Username = HttpContext.Session.GetString("username");
        //    return View();
        //}
        public IActionResult Registered()
        {
            ViewBag.Username = HttpContext.Session.GetString("username");
            return View();
        }
        public IActionResult Mail()
        {
            return View();
        }
        public IActionResult Single(int? id)
        {
            ViewBag.Username = HttpContext.Session.GetString("username");
            var sanpham = (from m in _context.SanPhamModel
                           where m.ID == id
                           select m).ToList();
            ViewBag.SanPham = sanpham;
            var Recent = (from m in _context.SanPhamModel
                          where m.MaLoai == 2
                          select m).Take(4).ToList();
            ViewBag.Recent = Recent;
            var BetsSell = (from l in _context.SanPhamModel
                            where l.DanhMuc == "DM3"
                            select l).Take(4).ToList();
            ViewBag.BestSellers = BetsSell;

            return View();
        }


        public async Task<IActionResult>search(String search)
        {
            ViewData["getSearch"] = search;
            var sch = from x in _context.SanPhamModel select x;
          
            if(! String.IsNullOrEmpty(search))
            {
                sch = sch.Where(x => x.TenSP.Contains(search));
            }
            return View(await sch.AsNoTracking().ToListAsync());
        }

        public const string CARTKEY = "cart";

        // Lấy cart từ Session (danh sách CartItem)
        List<ItemModel> GetCartItems()
        {

            var session = HttpContext.Session;
            string jsoncart = session.GetString(CARTKEY);
            if (jsoncart != null)
            {
                return JsonConvert.DeserializeObject<List<ItemModel>>(jsoncart);
            }
            return new List<ItemModel>();
        }

        // Xóa cart khỏi session
        void ClearCart()
        {
            var session = HttpContext.Session;
            session.Remove(CARTKEY);
        }

        // Lưu Cart (Danh sách CartItem) vào session
        void SaveCartSession(List<ItemModel> ls)
        {
            var session = HttpContext.Session;
            string jsoncart = JsonConvert.SerializeObject(ls);
            session.SetString(CARTKEY, jsoncart);
        }

        /// Thêm sản phẩm vào cart
        [Route("addcart/{productid:int}", Name = "addcart")]
        public IActionResult AddToCart([FromRoute] int productid)
        {

            var product = _context.SanPhamModel
                .Where(p => p.ID == productid)
                .FirstOrDefault();
            if (product == null)
                return NotFound("Không có sản phẩm");

            // Xử lý đưa vào Cart ...
            var cart = GetCartItems();
            var cartitem = cart.Find(p => p.SanPham.ID == productid);
            if (cartitem != null)
            {
                // Đã tồn tại, tăng thêm 1
                cartitem.QuanTity++;
            }
            else
            {
                //  Thêm mới
                cart.Add(new ItemModel() { QuanTity = 1, SanPham = product });
            }

            // Lưu cart vào Session
            SaveCartSession(cart);
            // Chuyển đến trang hiện thị Cart
            return RedirectToAction(nameof(Cart));      

        }

        // Hiện thị giỏ hàng
        [Route("/cart", Name = "cart")]
        public IActionResult Cart()
        {
            return View(GetCartItems());
        }



        /// Cập nhật
        [Route("/updatecart", Name = "updatecart")]
        [HttpPost]
        public IActionResult UpdateCart([FromForm] int productid, [FromForm] int quantity)
        {
            // Cập nhật Cart thay đổi số lượng quantity ...
            var cart = GetCartItems();
            var cartitem = cart.Find(p => p.SanPham.ID == productid);
            if (cartitem != null)
            {
                // Đã tồn tại, tăng thêm 1
                cartitem.QuanTity = quantity;
            }
            SaveCartSession(cart);
            // Trả về mã thành công (không có nội dung gì - chỉ để Ajax gọi)
            return Ok();
        }


        /// xóa item trong cart
        [Route("/removecart/{productid:int}", Name = "removecart")]
        public IActionResult RemoveCart([FromRoute] int productid)
        {
            var cart = GetCartItems();
            var cartitem = cart.Find(p => p.SanPham.ID == productid);
            if (cartitem != null)
            {
                // Đã tồn tại, tăng thêm 1
                cart.Remove(cartitem);
            }

            SaveCartSession(cart);
            return RedirectToAction(nameof(Cart));
        }


        [Route("/checkout")]
        public IActionResult CheckOut([FromForm] string email, [FromForm] string address)
        {

            // Xử lý khi đặt hàng
            var cart = GetCartItems();
            ViewData["email"] = email;
            ViewData["address"] = address;
            ViewData["cart"] = cart;

            if (!string.IsNullOrEmpty(email))
            {
                // hãy tạo cấu trúc db lưu lại đơn hàng và xóa cart khỏi session

                ClearCart();
                RedirectToAction(nameof(Index));
            }

            return View();
        }



    }
}
