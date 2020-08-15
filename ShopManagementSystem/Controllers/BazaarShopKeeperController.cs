using ShopManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ShopManagementSystem.Controllers
{
    public class BazaarShopKeeperController : Controller
    {
        buzzarEntities22 db1 = new buzzarEntities22();
        // GET: BazaarShopKeeper
        public ActionResult Index(int id)
        {

            return View();
        }

        // GET: BazaarShopKeeper/Details/5
        public ActionResult Details(int id)
        {
            
            TempData["idperson"] = id;
            ViewData["person"] = id;
            TempData["foredit"] = id;
            int person = id;
            Shoppack s = new Shoppack();
            s.shopKeeper = db1.ShopKeepers.Find(id);
            

            if (s.shopKeeper ==null)   //if shopkeeper has no shop
            {
                return RedirectToAction("Index", "BuyPackage", new { @id = person});
            }
            TempData["sid"] = s.shopKeeper.LoginId;
            s.shop= db1.Shops.Find(s.shopKeeper.ShopID);
            
            s.picture = db1.Pictures.Where(x => x.ShopID == s.shopKeeper.ShopID).FirstOrDefault();
            s.categorylist = db1.Categories.Where(x => x.ShopID == id).ToList();
          
                if (s.picture != null)
                {

                    return View(s);
                }
            return View();
           
        }
        public ActionResult Edit(int id)
        {
            ViewData["Backtoshop"] = TempData["idperson"];
            Shop a = db1.Shops.Find(id);
            if (a == null)
            {
                return HttpNotFound();
            }
            return View(a);

        }

        // POST: BazaarShopKeeper/Edit/5
        [HttpPost]
        public ActionResult Edit(Shop obj)
        {
            try
            {
                
                db1.Entry(obj).State = EntityState.Modified;
                db1.SaveChanges();
                ViewBag.Message = string.Format("Shop information is successfully updated!");
                return RedirectToAction("Details", new { id = (Convert.ToInt32(TempData["foredit"]))});

            }
            catch
            {
                ViewBag.Message = string.Format("Failed to update shop info!");
                return View();
            }
        }



        public static int login;
        //category list!
        public ActionResult CategoryIndex(int idshop)
        {
           
            TempData["ID"] = idshop;
            TempData["backlist"] = idshop;
            ShopKeeper s = db1.ShopKeepers.Where(x => x.ShopID == idshop).FirstOrDefault();
            login = s.LoginId;
            ViewData["personshop"] = login;
            
            return View(db1.Categories.ToList().Where(x => x.ShopID == idshop));
        }

        //add category
         public ActionResult Category()
        {
            ViewData["shid"] = TempData["sid"];
            return View();
        }
        [HttpPost]
        public ActionResult Category(Category obj)
        {
           
            try
            {
                
                
                if (ModelState.IsValid)
                {
                   
                    bool doesExistAlready = db1.Categories.Any(o => o.Name == obj.Name && o.Description==obj.Description);

                    if (!doesExistAlready)
                    {
                        obj.ShopID = Convert.ToInt32(TempData["ID"]);
                        var result1 = db1.Categories.Add(obj);
                        db1.SaveChanges();

                        ViewBag.Message = string.Format("Category is added successfully!");
                        ModelState.Clear();
                        return View();
                        

                    }
                    else
                    {
                        ViewBag.Message = string.Format("Failed to add category....category is already exist!");
                        return View();
                    }
                }
                ViewBag.Message = string.Format("Failed to add category!");
                return View();


            }
            catch
            {
                return View();
            }
        }

        public ActionResult CategoryEdit(int id)
        {
            Category a = db1.Categories.Find(id);
            if (a == null)
            {
                return HttpNotFound();
            }
            return View(a);
        }

        [HttpPost]
        public ActionResult CategoryEdit(Category obj)
        {
            try
            {

               
                db1.Entry(obj).State = EntityState.Modified;
                db1.SaveChanges();
                ViewBag.Message = string.Format("Category information is successfully updated!");
                return RedirectToAction("CategoryIndex", new { idshop = obj.ShopID});

            }
            catch
            {
                ViewBag.Message = string.Format("Failed to update category info!");
                return View();
            }
        }



        
        public ActionResult profile(int id)
        {
            Shoppack a=new Shoppack();
            a.shopKeeper = db1.ShopKeepers.Where(x => x.ShopID ==id).FirstOrDefault();
            int login=a.shopKeeper.LoginId;
            a.person = db1.Logins.Find(a.shopKeeper.LoginId);
            return View(a);

        }
        public ActionResult profileupdate(int id)
        {
            Login a = db1.Logins.Find(id);
            TempData["idper"]=id;
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

                    db1.Entry(obj).State = EntityState.Modified;
                    db1.SaveChanges();



                    return RedirectToAction("Details", new { id = idperson });

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
        public ActionResult ShopPicture(int id)
        {
            Shoppack a = new Shoppack();
            a.picture = db1.Pictures.Where(x => x.ShopID == id).FirstOrDefault();
            a.shopKeeper = db1.ShopKeepers.Where(x => x.ShopID == id).FirstOrDefault();
            TempData["shopkeeperid"] = a.shopKeeper.LoginId;
            TempData["shopid"] = a.picture.ShopID;
            if(a.picture.picture1!=null)
            {
               
                return View(a);
            }
            return View();
           
        }
        [HttpPost]
        public ActionResult ShopPicture(Picture obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int shopkeeperid = Convert.ToInt32(TempData["Shopkeeperid"]);
                    obj.ShopID = Convert.ToInt32(TempData["Shopid"]);
                    obj.picture1 = new byte[obj.ImageFile1.ContentLength];
                    obj.ImageFile1.InputStream.Read(obj.picture1, 0, obj.ImageFile1.ContentLength);
          
                    db1.Entry(obj).State = EntityState.Modified;
                    db1.SaveChanges();
                    return RedirectToAction("Details", new { id = shopkeeperid });
                }
                ViewBag.Message = string.Format("Failed to update a shop picutre!");
                return View();

            }
            catch
            {
                return View();
            }
        }

        public ActionResult ItemIndex(int id)  //id is category id
        {
            ItemPicture a = new ItemPicture();
            a.itemlist = db1.Items.Where(x => x.CategoryId == id).ToList();
            Category b = db1.Categories.Find(id);

            ViewData["shoop"] = b.ShopID;
            TempData["back"] = id;  //for a back from item delete
               
            ViewData["CategoryId"] = id;
            TempData["cid"] = id;
           
            return View(a);
           
            
            
           
        }
        // GET: BazaarShopKeeper/Create
        public ActionResult AddItem(int id)  //id is category id
        {
            TempData["afteradd"] = id;
            ViewData["Categoryid"] = id;
            TempData["catid"] = id;
            return View();
        }

        // POST: BazaarShopKeeper/Create
        [HttpPost]
        public ActionResult AddItem(ItemPicture obj)
        {
            using (var transaction = db1.Database.BeginTransaction())
            {
                ViewData["Categoryid"] = TempData["afteradd"];
                try
                {

                    if (ModelState.IsValid)
                    {
                        bool doesExistAlready = db1.Items.Any(o => o.Name == obj.item.Name && o.Description == obj.item.Description);

                        if (!doesExistAlready)
                        {
                            
                            obj.item.CategoryId = Convert.ToInt32(TempData["catid"]);
                            
                            var result0 = db1.Items.Add(obj.item);
                            //int itemid = obj.item.Id;
                            db1.SaveChanges();

                            if (obj.picture1.ImageFile1 != null)
                            {

                                obj.picture1.picture1 = new byte[obj.picture1.ImageFile1.ContentLength];
                                obj.picture1.ImageFile1.InputStream.Read(obj.picture1.picture1, 0, obj.picture1.ImageFile1.ContentLength);

                                obj.picture1.ItemID = obj.item.Id;

                                var result3 = db1.Pictures.Add(obj.picture1);
                                db1.SaveChanges();

                                if (obj.picture2.ImageFile1 != null)
                                {

                                    obj.picture2.picture1 = new byte[obj.picture2.ImageFile1.ContentLength];
                                    obj.picture2.ImageFile1.InputStream.Read(obj.picture2.picture1, 0, obj.picture2.ImageFile1.ContentLength);

                                    obj.picture2.ItemID = obj.item.Id;

                                    var result6 = db1.Pictures.Add(obj.picture2);
                                    db1.SaveChanges();



                                }

                                if (obj.picture3.ImageFile1 != null)
                                {

                                    obj.picture3.picture1 = new byte[obj.picture3.ImageFile1.ContentLength];
                                    obj.picture3.ImageFile1.InputStream.Read(obj.picture3.picture1, 0, obj.picture3.ImageFile1.ContentLength);

                                    obj.picture3.ItemID = obj.item.Id;

                                    var result5 = db1.Pictures.Add(obj.picture3);
                                    db1.SaveChanges();



                                }

                                if (obj.picture4.ImageFile1 != null)
                                {

                                    obj.picture4.picture1 = new byte[obj.picture4.ImageFile1.ContentLength];
                                    obj.picture4.ImageFile1.InputStream.Read(obj.picture4.picture1, 0, obj.picture4.ImageFile1.ContentLength);

                                    obj.picture4.ItemID = obj.item.Id;

                                    var result10 = db1.Pictures.Add(obj.picture4);
                                    db1.SaveChanges();



                                }

                                if (obj.picture5.ImageFile1 != null)
                                {

                                    obj.picture5.picture1 = new byte[obj.picture5.ImageFile1.ContentLength];
                                    obj.picture5.ImageFile1.InputStream.Read(obj.picture5.picture1, 0, obj.picture5.ImageFile1.ContentLength);

                                    obj.picture5.ItemID = obj.item.Id;

                                    var result9 = db1.Pictures.Add(obj.picture5);
                                    db1.SaveChanges();

                                    

                                }
                                transaction.Commit();
                                ViewBag.Message = string.Format("Item is successfully added!");
                                return View();
                            }

                        }
                        else
                        {
                            ViewBag.Message = string.Format("Failed to add item...Item is already exist!");
                            return View();
                        }
                    }
                    ViewBag.Message = string.Format("Failed to add item!");
                    return View();

                }
                catch
                {
                    transaction.Rollback();
                    return View();
                }
            }
        }
      

        public ActionResult ItemDetails(int id)
        {
            ViewData["item"] = TempData["cid"];

            ItemPicture a = new ItemPicture();
            a.item = db1.Items.Where(x => x.Id == id).FirstOrDefault();

            a.picturelist = db1.Pictures.Where(x => x.ItemID == id).ToList();
            
            return View(a);
     
           
        }

        public ActionResult ItemEdit(int id)
        {
            Item a = db1.Items.Find(id);
            if (a == null)
            {
                return HttpNotFound();
            }
            return View(a);
        }

        [HttpPost]
         public ActionResult ItemEdit(Item obj)
        {
            try
            {

                db1.Entry(obj).State = EntityState.Modified;
                db1.SaveChanges();
                ViewBag.Message = string.Format("Item information is successfully updated!");
                return RedirectToAction("ItemIndex", new { id = obj.CategoryId });

            }
            catch
            {
                ViewBag.Message = string.Format("Failed to update item info!");
                return View();
            }
        }

        public ActionResult ItemDelete(int id)
        {
            
            ItemPicture a = new ItemPicture();
            a.item = db1.Items.Find(id);
            a.picturelist = db1.Pictures.Where(x => x.ItemID == id).ToList();
            if (a.item == null)
            {
                return HttpNotFound();
            }
            return View(a);
        }

        [HttpPost, ActionName("ItemDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {

                ItemPicture obj = new ItemPicture();

                obj.picturelist = db1.Pictures.Where(x => x.ItemID == id).ToList();
                for (int i = 0; i < obj.picturelist.Count;i++ )
                {
                    db1.Pictures.Remove(obj.picturelist[i]);
                   
                }
                db1.SaveChanges();
                if(id!=null)
                {
                    obj.item = db1.Items.Find(id);
                    db1.Items.Remove(obj.item);
                    db1.SaveChanges();
                    return RedirectToAction("ItemIndex", new { id = (Convert.ToInt32(TempData["back"])) });
                }
                ViewBag.Message = string.Format("Unable to delete item!");
                return View();
               
               

            }
            catch
            {
                ViewBag.Message = string.Format("Failed to delete item!");
                return View();
            }
        }

        public ActionResult CategoryDelete(int id)
        {
            Shoppack a = new Shoppack();
           
            a.category= db1.Categories.Find(id);
            a.itemlist = db1.Items.Where(x => x.CategoryId == id).ToList();
            a.item = db1.Items.Where(x => x.CategoryId == id).FirstOrDefault();
            if (a.category == null)
            {
                return HttpNotFound();
            }
            return View(a);
        }

        [HttpPost, ActionName("CategoryDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult CategoryDel(int id)
        {
            try
            {

                Shoppack obj = new Shoppack();

                 obj.itemlist = db1.Items.Where(x => x.CategoryId == id).ToList();
                 if (obj.itemlist != null)
                 {
                     int i = 0;
                     do
                     {
                         obj.picturelist = db1.Pictures.Where(x => x.ItemID == obj.itemlist[i].Id).ToList();
                         for (int k = 0; k < obj.picturelist.Count; k++)
                         {
                             db1.Pictures.Remove(obj.picturelist[k]);
                             
                         }
                         db1.SaveChanges();
                         i = i + 1;
                     } while (i < obj.itemlist.Count);

                     for (int j = 0; j < obj.itemlist.Count; j++)
                     {
                         db1.Items.Remove(obj.itemlist[j]);
                         
                     }
                     db1.SaveChanges();
                 }
                if (id != null)
                {
                    obj.category = db1.Categories.Find(id);
                    db1.Categories.Remove(obj.category);
                    db1.SaveChanges();
                    return RedirectToAction("CategoryIndex", new { idshop = (Convert.ToInt32(TempData["backlist"])) });
                }
                ViewBag.Message = string.Format("Unable to delete category!");
                return View();



            }
            catch
            {
                ViewBag.Message = string.Format("Failed to delete category!");
                return View();
            }
        }
        
    }
}
