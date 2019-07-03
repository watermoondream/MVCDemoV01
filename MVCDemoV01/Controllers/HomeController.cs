using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCDemoV01.Models;
using PagedList;


namespace MVCDemoV01.Controllers
{
    
    public class HomeController : Controller
    {
        northwindEntities db = new northwindEntities();
        int pageSize = 20;

        // GET: Home
        public ActionResult Index(int page=1)
        {
            int currentPage = page < 1 ? 1 : page;
            var customers = db.Customers.OrderBy(m=>m.CustomerID).ToList();
            var result = customers.ToPagedList(currentPage, pageSize);
            return View("Index",result);
        }

        //GET: Create
        public ActionResult Create()
        {
            return View();
        }

        //POST:Create
        [HttpPost]
        public ActionResult Create(string CustomerID, string CompanyName, string ContactName, string ContactTitle, string Country, string City, string Region, string Address, string PostalCode, string Phone, string Fax)
        {
            bool isError = false;
            if (string.IsNullOrWhiteSpace(CustomerID))
            {
                ModelState.AddModelError("CustomerID","客戶編號為必填");
                isError = true;
            }
            else
            {
                ModelState.AddModelError("CustomerID", "");
            }

            if (string.IsNullOrWhiteSpace(CompanyName))
            {
                ModelState.AddModelError("CompanyName", "公司名稱為必填");
                isError = true;
            }
            else
            {
                ModelState.AddModelError("CompanyName", "");
            }

            if (isError)
            {
                return View();
            }
            Customers cust = new Customers();
            cust.CustomerID = CustomerID;
            cust.CompanyName = CompanyName;
            cust.ContactName = ContactName;
            cust.ContactTitle = ContactTitle;
            cust.Country = Country;
            cust.City = City;
            cust.Region = Region;
            cust.Address = Address;
            cust.PostalCode = PostalCode;
            cust.Phone = Phone;
            cust.Fax = Fax;
            db.Customers.Add(cust);
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                ViewBag.Message = "新增資料發生錯誤："+ex.Message;
                return View();
            }
            return RedirectToAction("Index");
        }

        //GET: Delete
        public ActionResult Delete(string CustomerID)
        {
            var cust = db.Customers.Where(m => m.CustomerID == CustomerID).FirstOrDefault();
            db.Customers.Remove(cust);
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                ViewBag.Message = "刪除資料發生錯誤：" + ex.Message;
                return View();
            }
            return RedirectToAction("Index");
        }

        //GET: Detail
        public ActionResult Detail(string CustomerID)
        {

            var cust = db.Customers.Where(m => m.CustomerID == CustomerID).FirstOrDefault();
            if (cust == null)
            {
                ViewBag.Message = "找不到該筆客戶資料";
                return View("Detail");
            }
            return View("Detail",cust);
        }

        //GET: Edit
        public ActionResult Edit(string CustomerID)
        {
            var cust = db.Customers.Where(m => m.CustomerID == CustomerID).FirstOrDefault();
            if (cust == null)
            {
                ViewBag.Message = "找不到該筆客戶資料";
                return View("Edit");
            }
            return View("Edit", cust);
        }

        [HttpPost]
        public ActionResult Edit(string CustomerID, string CompanyName, string ContactName, string ContactTitle, string Country, string City, string Region, string Address, string PostalCode, string Phone, string Fax)
        {
            bool isError = false;
            if (string.IsNullOrWhiteSpace(CompanyName))
            {
                ModelState.AddModelError("CompanyName", "公司名稱為必填");
                isError = true;
            }
            else
            {
                ModelState.AddModelError("CompanyName", "");
            }
            if (isError)
            {
                return View();
            }
            var cust = db.Customers.Where(m => m.CustomerID == CustomerID).FirstOrDefault();
            if (cust == null)
            {
                ViewBag.Message = "找不到該筆客戶資料";
                return View("Edit");
            }
            cust.CompanyName = CompanyName;
            cust.ContactName = ContactName;
            cust.ContactTitle = ContactTitle;
            cust.Country = Country;
            cust.City = City;
            cust.Region = Region;
            cust.Address = Address;
            cust.PostalCode = PostalCode;
            cust.Phone = Phone;
            cust.Fax = Fax;
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                ViewBag.Message = "修改資料發生錯誤：" + ex.Message;
                return View();
            }

            return RedirectToAction("Index");
        }


    }
}