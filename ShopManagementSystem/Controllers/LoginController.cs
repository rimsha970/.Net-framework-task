
using ShopManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;


namespace ShopManagementSystem.Controllers
{
    public class LoginController : Controller
    {
        buzzarEntities8 db = new buzzarEntities8();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        // GET: Login/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Login/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Login/Create
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

        // GET: Login/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Login/Edit/5
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

        // GET: Login/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Login/Delete/5
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

        public ActionResult view()
        {
            return View();
        }

        // POST: Login/Delete/5
        [HttpPost]
        public ActionResult view(Login collection)
        {
            try
            {
                // TODO: Add login logic here
                var result=db.Logins.Where(m=>m.Email==collection.Email && m.Password==collection.Password).FirstOrDefault();       
                if (result != null) 
                {   
                    if(result.LoginType=="Admin")
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                    else if (result.LoginType == "Shopkeeper")
                    {
                        return RedirectToAction("Details", "BazaarShopKeeper", new { @id = result.Id });
                    }
                    
                 return RedirectToAction("Index", "Customer", new { @id = result.Id });

                }

                else

                {

                  ViewBag.Message = string.Format("UserName or Password is incorrect");

                  return View();

                }

                
            }
            catch
            {
                return View();
            }
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(Login obj)
        {
            Login a = db.Logins.Where(x => x.SecurityAnswer == obj.SecurityAnswer && x.SecurityQuestion == obj.SecurityQuestion && x.Email == obj.Email).FirstOrDefault();
            if(a!=null)
            {
                a.Password = obj.Password;
                db.Entry(a).State = EntityState.Modified;
                db.SaveChanges();
                
                ViewBag.Message = string.Format("Password is changed successfully!!");
                return RedirectToAction("view", "Login");
               
            
            }
            ViewBag.Message = string.Format("Incorrect Email or Answer to Security Question for the recovery of your account");
            return view();
           
            
        }

       
    }
}
