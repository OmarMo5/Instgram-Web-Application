using instgaram.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace instgaram.Controllers
{
    public class HomeController : Controller
    {
        private Model db = new Model();

        public ActionResult Index()
        {
            return View();
        }
        
        [HttpGet]
        public ActionResult LogIn()
        {
            Session["Userid"] = "0";
            return View();
        }
        [HttpPost]
        public ActionResult LogIn(String username, String password)
        {
            User user = db.Users.Where(u => u.Username == username && u.Password == password).First();
            if (user == null)
            {
                ViewBag.Message = "your pass not correct";
                return View();
            }
            Session["Userid"] = user.Id.ToString();
            return RedirectToAction("Index", "Porfile");
        }


        [HttpGet]
        public ActionResult Signup()
        {
            Session["Stuts"] = "0";
            Session["Userid"] = "0";
            return View();
        }
        [HttpPost]
        public ActionResult Signup(FormCollection form,string Repass,HttpPostedFileBase photo)
        {
            if (form == null)
            {
                return RedirectToAction("Index","Home");
            }
            User u = new User();
            u.FName = form["Fname"].ToString(); 
            u.LName = form["Lname"].ToString(); 
            u.Username = form["username"].ToString();
            u.Mobile = form["Phone"].ToString();
            if ( form["password"].ToString() != form["repassword"].ToString() )
            {
                return RedirectToAction("Index","Home");
            }
            u.Password = form["password"].ToString();
            
            HttpPostedFileBase postedFile = Request.Files["photo"];
            string path = Server.MapPath("~/Uploads/");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            postedFile.SaveAs(path + Path.GetFileName(postedFile.FileName)); 
            u.Photo = "/Uploads/" + Path.GetFileName(postedFile.FileName).ToString();
            
            db.Users.Add(u);
            db.SaveChanges();
            ViewBag.mss = "your accunt is created,Congratulation!!☻";
            return View();
        }
    }
}