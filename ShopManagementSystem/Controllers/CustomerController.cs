using ShopManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopManagementSystem.Controllers
{
    public class CustomerController : Controller
    {
        buzzarEntities22 db = new buzzarEntities22();
        // GET: Customer
        public ActionResult Index(int id)
        {
            ViewData["login"] = id;
            Shoppack a = new Shoppack();
            a.customer = db.Customers.Find(id);
            a.categorylist = db.Categories.ToList();
            a.itemlist = db.Items.ToList();
            a.picturelist = db.Pictures.Where(x => x.ItemID != null).ToList();
            return View(a);
        }

        public ActionResult wishlist(int id,int itemid)  //login id
        {
            TempData["id"] = id;
            TempData["itemid"] = itemid;
            return View();
        }
        [HttpPost]
        public ActionResult wishlist(Saved obj) 
        {
            obj.CustomerId = Convert.ToInt32(TempData["id"]);
            obj.ItemId = Convert.ToInt32(TempData["itemid"]);
            
            var result10 = db.Saveds.Add(obj);
            db.SaveChanges();
            
            ViewBag.Message = string.Format("Item is saved successfully!!");
            return RedirectToAction("Index", "Customer", new { id = obj.CustomerId });
        }
        // GET: Customer/Details/5
        public ActionResult profile(int id)
        {
            Shoppack a = new Shoppack();
            a.customer = db.Customers.Where(x => x.LoginId == id).FirstOrDefault();
            int login = a.customer.LoginId;
            a.person = db.Logins.Find(a.customer.LoginId);
            return View(a);
        }
        public ActionResult profileupdate(int id)
        {
            Login a = db.Logins.Find(id);
            TempData["idper"] = id;
            if (a == null)
            {
                return HttpNotFound();
            }
            return View(a);



        }
        [HttpPost]
        public ActionResult profileupdate(Login obj)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    int idperson = Convert.ToInt32(TempData["idper"]);

                    db.Entry(obj).State = EntityState.Modified;
                    db.SaveChanges();



                    return RedirectToAction("profile", new { id = idperson });

                }
                catch
                {
                    ViewBag.Message = string.Format("Failed to update your profie!");
                    return View();
                }
            }
            ViewBag.Message = string.Format("Invalid information");
            return View();
        }

        public ActionResult savelist(int id)
        {
            int login=id;
            Shoppack a = new Shoppack();
            a.savelist = db.Saveds.Where(s => s.CustomerId == id).ToList();
            if (a.savelist != null)
            {
                a.itemlist = db.Items.ToList();


                return View(a);
            }
            else
            {
                ViewBag.Message = string.Format("Your wish list is empty!");
                return RedirectToAction("Index", new { id = login });
            }
        }
       

        // GET: Customer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customer/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Customer/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Customer/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Customer/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Customer/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
