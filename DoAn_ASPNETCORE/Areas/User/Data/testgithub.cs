﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DoAn_ASPNETCORE.Areas.User.Data
{
    public class testgithub : Controller
    {
        // GET: testgithub
        public ActionResult Index()
        {
            return View();
        }

        // GET: testgithub/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: testgithub/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: testgithub/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: testgithub/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: testgithub/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: testgithub/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: testgithub/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
