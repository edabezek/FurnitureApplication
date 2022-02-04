using Furniture.Data;
using Furniture.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Furniture.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _dB;
        public CategoryController(ApplicationDbContext dB)
        {
            _dB = dB;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> objList = _dB.Category;
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
        public IActionResult Create(Category obj)
        {
            if (ModelState.IsValid)
            {
                _dB.Category.Add(obj);
                _dB.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        //get-edit
        public IActionResult Edit(int id)
        {
            if(id==null || id==0)
            {
                return NotFound();
            }
            var obj = _dB.Category.Find(id);
            if(obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        //post-edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _dB.Category.Update(obj);
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
            var obj = _dB.Category.Find(id);
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
            var obj = _dB.Category.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _dB.Category.Remove(obj);
            _dB.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
