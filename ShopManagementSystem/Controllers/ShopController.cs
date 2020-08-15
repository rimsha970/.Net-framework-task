using ShopManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.IO;


namespace ShopManagementSystem.Controllers
{
   
    public class ShopController : Controller
    {

        public int shopid;
        buzzarEntities22 db = new buzzarEntities22();
       
       
        // GET: Shop
        public ActionResult Index()
        {
           
            return View();
        }

        // GET: Shop/Details/5
        public ActionResult Details(int id)
        {
            
            return View();
        }

        // GET: Shop/Create
        public ActionResult Create1(int id6, int id7, int? id8)
        {
            if (id6 != 0 && id7 != 0)
            {
                TempData["person"] = id6;
                TempData["id"] = id7;
                TempData["extra"] = id8;
                return View();
            }
            else
            {
                return RedirectToAction("Index", "BuyPackage");
            }
        }

        // POST: Shop/Create
        [HttpPost]
        public ActionResult Create1(ShopManagementSystem.Models.Shop b)
        {
            using (var transaction = db.Database.BeginTransaction())
            {

                try
                {
                    b.SubPackage = Convert.ToInt32(TempData["id"]);
                    if (TempData["extra"] != null)
                    {
                        b.AppPackage = Convert.ToInt32(TempData["extra"]);
                    }
                    else
                    {
                        b.AppPackage = null;
                    }

                    int login = Convert.ToInt32(TempData["person"]);
                    var result4 = db.Shops.Add(b);
                    db.SaveChanges();
                    shopid = result4.Id;  //get shopid
                    transaction.Commit();
                    return RedirectToAction("Create3", new { @id11 = shopid, @id0 = login });


                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    ViewBag.Message = string.Format("Failed to add your shop!");
                    return View();
                }
            }
            
        }
        public ActionResult Create3(int id11,int id0)
        {
            TempData["shop"] = id11;
            TempData["log"] = id0;
            return View();
        }
        [HttpPost]
        public ActionResult Create3(ShopKeeper c)
        {
            c.LoginId = Convert.ToInt32(TempData["log"]);
            c.ShopID = Convert.ToInt32(TempData["shop"]);
            if(c.ImageFile2!=null)
            {
                
                c.picture = new byte[c.ImageFile2.ContentLength];
                c.ImageFile2.InputStream.Read(c.picture, 0, c.ImageFile2.ContentLength);
                var result10 = db.ShopKeepers.Add(c);
                db.SaveChanges();
                return RedirectToAction("Create4", new { @id1 = c.ShopID });
              
            }
            else
            {
                var result6 = db.ShopKeepers.Add(c);
                db.SaveChanges();
                return RedirectToAction("Create4", new { @id1 = c.ShopID });
            }
            return View();
            
        
            
        }

        public ActionResult Create4(int id1)
        {
            TempData["s"] = id1;
           
            return View();
        }
        

        [HttpPost]
        public ActionResult Create4(Picture a)
        {
                    // TODO: Add insert logic here
              if (a.ImageFile1 != null)
              {

                a.picture1 = new byte[a.ImageFile1.ContentLength];
                a.ImageFile1.InputStream.Read(a.picture1, 0, a.ImageFile1.ContentLength);
                
                a.ShopID = Convert.ToInt32(TempData["s"]);

                var result3 = db.Pictures.Add(a);
                db.SaveChanges();
                int pictureid = result3.Id;  //get pictureid
                return RedirectToAction("view", "Login");

              }
              return View();
                   
        }

        
       
        // GET: Shop/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Shop/Edit/5
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

        // GET: Shop/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Shop/Delete/5
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
