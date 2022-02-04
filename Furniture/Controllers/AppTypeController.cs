using Furniture.Data;
using Furniture.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Furniture.Controllers
{
    public class AppTypeController : Controller
    {
        private readonly ApplicationDbContext _dB;
        public AppTypeController(ApplicationDbContext dB)
        {
            _dB = dB;
        }
        public IActionResult Index()
        {
            IEnumerable<ApplicationType> objList = _dB.AppType;
            return View(objList);
        }
        //get-create
        public IActionResult Create()
        {
            return View();
        }
        //post-create
        [HttpPost]
        [ValidateAntiForgeryToken]//built in security
        public IActionResult Create(ApplicationType obj)
        {
            if (ModelState.IsValid)
            {
                _dB.AppType.Add(obj);
                _dB.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        //get-edit
        public IActionResult Edit(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _dB.AppType.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        //post-edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ApplicationType obj)
        {
            if (ModelState.IsValid)
            {
                _dB.AppType.Update(obj);
                _dB.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        //get-deete
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _dB.AppType.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        //post-delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _dB.AppType.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _dB.AppType.Remove(obj);
            _dB.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
