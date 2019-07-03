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
    }
}