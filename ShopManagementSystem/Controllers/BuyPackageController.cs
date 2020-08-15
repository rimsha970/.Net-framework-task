using ShopManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ShopManagementSystem.Controllers
{
    public class BuyPackageController : Controller
    {
        
        
        buzzarEntities10 db = new buzzarEntities10();
        public int personid{get;set;}
        public  int apppackage{get;set;}
        public  int subpackage { get; set; }
        // GET: BuyPackage
        public ActionResult Index(int id)
        {
            ViewData["Id"] = id;
            return View(db.Packages.ToList());
            
            
        }

        public ActionResult ExtraPackage(int id1,string id2)
        {
            apppackage = id1;
           // int apppackage = (int)HttpContext.Session[id1];
            if (id1 != 0)
            {
                ViewData["Id1"] = id1;    //id1 is packageid
                ViewData["Id2"] = id2;
                //ViewBag.Id2 = id2;    //id2 is person id 
                return View(db.Packages.ToList());
            }
            ViewBag.Message = string.Format("Failed to buy a package");
            return RedirectToAction("Index", "BuyPackage");


        }

        public ActionResult Package(string id3,string id4,int? id5)
        {
            //int sub = (int)HttpContext.Session[id2];
            apppackage = Convert.ToInt32(id3);  //1st package
            personid = Convert.ToInt32(id4);    //personid
            //subpackage = id5;  //extra package
            if (apppackage != 0 && personid != 0)
            {
                
                return RedirectToAction("Create1", "Shop", new { @id6 = personid, @id7 = apppackage, @id8 = id5 });
            }
           
            ViewBag.Message = string.Format("Failed to buy an extra package");
            return View();


        }
       
        


        // GET: BuyPackage/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: BuyPackage/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BuyPackage/Create
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

        // GET: BuyPackage/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BuyPackage/Edit/5
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

        // GET: BuyPackage/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BuyPackage/Delete/5
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
        /**public ActionResult view()
        {
            
            return View();
        }

        // POST: Login/Delete/5
        [HttpPost]
        public ActionResult view(Package collection)
        {
            try
            {

                

                // TODO: Add login logic here
                var result = db.Packages.Where(m => m.Name == collection.Name && m.Price == collection.Price).FirstOrDefault();
                if (result != null)
                {
                        apppackage = result.Id;
                        return RedirectToAction("Index", "Admin");
                    

                }

                else
                {

                    ViewBag.Message = string.Format("Failed to buy a package!");

                    return View();

                }


            }
            catch
            {
                return View();
            }
        }*/
    }
}
