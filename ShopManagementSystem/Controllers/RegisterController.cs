using ShopManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopManagementSystem.Controllers
{
    
    public class RegisterController : Controller
    {
        public int personid; 
        buzzarEntities9 db = new buzzarEntities9();
        // GET: Register
        public ActionResult Index()
        {
            return View();
        }

        // GET: Register/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Register/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Register/Create
        [HttpPost]
        public ActionResult Create(Login obj1, Customer obj2)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    bool doesExistAlready = db.Logins.Any(o => o.Name == obj1.Name && o.Email == obj1.Email && o.LoginType == o.LoginType);

                    if (!doesExistAlready)
                    {

                        personid=obj1.Id;
                        var result1 = db.Logins.Add(obj1);
                        db.SaveChanges();
                        personid = result1.Id;
                        if (result1.LoginType == "Customer")
                        {
                            obj2.LoginId = personid;
                            var result2 = db.Customers.Add(obj2); 
                            db.SaveChanges();
                            return RedirectToAction("view", "Login");
                        }
                        else if (result1.LoginType == "Shopkeeper")
                        {

                            return RedirectToAction("Index", "BuyPackage", new { @id = obj1.Id });
                        }
                        return RedirectToAction("view", "Login");
                    }
                    else
                    {
                        ViewBag.Message = string.Format("Failed to Signup....Account is already exist with this email!");
                        return View();
                    }
                }
                ViewBag.Message = string.Format("Failed to Signup! You are trying to enter some invalid information");
                return View();
            }
            catch
            {
                return View();
            }
            
        }

        // GET: Register/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Register/Edit/5
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

        // GET: Register/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Register/Delete/5
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
