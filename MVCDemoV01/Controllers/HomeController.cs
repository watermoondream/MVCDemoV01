using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCDemoV01.Models;


namespace MVCDemoV01.Controllers
{
    
    public class HomeController : Controller
    {
        northwindEntities db = new northwindEntities();

        // GET: Home
        public ActionResult Index()
        {
            var customers = db.Customers.ToList();           
            return View("Index",customers);
        }

        //GET: Create
        public ActionResult Create()
        {
            return View();
        }

        //POST:Create
        [HttpPost]
        public ActionResult Create(Customers cust)
        {
            db.Customers.Add(cust);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}