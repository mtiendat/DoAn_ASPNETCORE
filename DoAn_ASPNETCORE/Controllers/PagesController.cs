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
            
            ViewBag.id = id;
            ViewBag.Username = HttpContext.Session.GetString("username");
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
            ViewBag.id = id;
            //var DsLaptop = (from m in _context.SanPhamModel
            //                where m.MaLoai == id
            //                select m).ToList();
            //ViewBag.LapTop = DsLaptop;
            ViewBag.Username = HttpContext.Session.GetString("username");
            return View();
        }
        public IActionResult Login()
        {
            ViewBag.Username = HttpContext.Session.GetString("username");
            return View();
        }
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





    }
}
