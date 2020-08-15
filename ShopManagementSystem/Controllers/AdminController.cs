using ShopManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopManagementSystem.Controllers
{
    public class AdminController : Controller
    {
       
        buzzarEntities10 db = new buzzarEntities10();
        // GET: Admin
        public ActionResult Index()
        {
            return View(db.Packages.ToList());
           
        }

        // GET: Admin/Details/5
        public ActionResult Details(int id=0)
        {
            Package a = db.Packages.Find(id);
            if (a==null)
            {
                return HttpNotFound();
            }
            return View(a);
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
        [HttpPost]
        public ActionResult Create(Package obj)
        {
            try
            {
                if (ModelState.IsValid)                       //checks validations
                {
                    bool doesExistAlready = db.Packages.Any(o => o.Name == obj.Name && o.Price==obj.Price);

                    if (!doesExistAlready)
                    {
                        var result1 = db.Packages.Add(obj);
                        db.SaveChanges();
                        ViewBag.Message = string.Format("Package is added successfully!!");
                        return View();
                       
                           
                        
                    }
                    else
                    {
                        ViewBag.Message = string.Format("Failed to add package....Package is already exist!");
                        return View();
                    }
                }
                ViewBag.Message = string.Format("Failed to add package!");
                return View();
                
               
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Edit/5
        public ActionResult Edit(int id=0)
        {
            Package a = db.Packages.Find(id);
            if (a == null)
            {
                return HttpNotFound();
            }
            return View(a);
        }

        // POST: Admin/Edit/5
        [HttpPost]
        public ActionResult Edit(Package obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(obj).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index", "Admin");
                }
                ViewBag.Message = string.Format("Failed to update a package!");
                return View(obj);

            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Delete/5
        public ActionResult Delete(int id=0)
        {
            Package a = db.Packages.Find(id);
            if (a == null)
            {
                return HttpNotFound();
            }
            return View(a);
        }

        // POST: Admin/Delete/5
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {

                Package result = db.Packages.Find(id);
                db.Packages.Remove(result);
                db.SaveChanges();
                return RedirectToAction("Index", "Admin");

            }
            catch
            {
                ViewBag.Message = string.Format("Failed to delete package!");
                return View();
            }
        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
