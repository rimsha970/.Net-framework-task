using ShopManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        buzzarEntities22 db = new buzzarEntities22();
        public ActionResult Index()
        {
            Shoppack a = new Shoppack();
            a.categorylist = db.Categories.ToList();
            a.itemlist = db.Items.ToList();
            a.picturelist = db.Pictures.Where(x => x.ItemID != null ).ToList();
            
            return View(a);
        }

        public ActionResult ItemDetails(int id)
        {
           
            ItemPicture a = new ItemPicture();
            a.item = db.Items.Where(x => x.Id == id).FirstOrDefault();
            ViewData["item"] = a.item.Name;
            a.picturelist = db.Pictures.Where(x => x.ItemID == id).ToList();

            return View(a);


        }

        public ActionResult Categories(int id)
        {
            Category obj = db.Categories.Find(id);
            ViewData["name"] = obj.Name;
            ItemPicture a = new ItemPicture();
            a.itemlist = db.Items.Where(x => x.CategoryId == id).ToList();
            a.picturelist = db.Pictures.Where(x => x.ItemID != null).ToList();
            return View(a);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Help()
        {
            ViewBag.Message = "Help!!!";

            return View();
        }
    }
}