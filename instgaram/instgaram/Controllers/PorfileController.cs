using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using instgaram.Models;
/*
==any image is personal photo in ->          Uploads Folder
==any image is post in your profile is in -> Photopost Folder
*/

namespace instgaram.Controllers
{
    public class PorfileController : Controller
    {
        private Model db = new Model();

        public ActionResult Index()
        {
            if(Session["Userid"] == "0" && Session["Userid"]==null)
            {
                return RedirectToAction("Index", "Home");
            }
            User u =db.Users.Find(Convert.ToInt32(Session["Userid"]));
            return View(u);
        }

        //To see all person In web App and send Request
        public ActionResult allpersons()
        {
            int x = Convert.ToInt32(Session["Userid"]);
            List<User> users = db.Users.Where(
                m => m.Id != x
                ).ToList();

            List<friend_re> friend_Res = db.friend_Res.
                Where(m => m.sender1_id == x).ToList();

            foreach (var item in friend_Res)
            {
                users.Remove(item.resever);
            }
            return View(users);
        }

        //To can Add Post in Your Profile
        [HttpGet]
        public ActionResult addpost()
        {
            if (Session["Userid"] == "0" && Session["Userid"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpPost]
        public ActionResult addpost(HttpPostedFileBase photo)
        {
            HttpPostedFileBase postedFile = Request.Files["photo"];
            string path = Server.MapPath("~/photopost/");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            postedFile.SaveAs(path + Path.GetFileName(postedFile.FileName));
            post post = new post();
            post.photo = "/photopost/" + Path.GetFileName(postedFile.FileName);
            post.user = db.Users.Find(Convert.ToInt32(Session["Userid"]));
            db.posts.Add(post);
            db.SaveChanges();
            return RedirectToAction("index");
        }
                
                //To see Person That send to you Request
        public ActionResult my_re()
        {
            int x = Convert.ToInt32(Session["Userid"]);
            List<friend_re> friend_Res =db.friend_Res.Where(
                m => m.sender1_id == x).ToList();
            return View(friend_Res);
        }
        public ActionResult addfriend(int? id)
        {
            friend_re friend_Re = new friend_re();

            int x = Convert.ToInt32(Session["Userid"]);
            int idi = Convert.ToInt32(id);
            try
            {
               List<friend_re> g =
                    db.friend_Res.Where(
                        m => m.sender1_id == x && m.resever1_id == idi
                        ).ToList();
                db.friend_Res.RemoveRange(g);
                db.SaveChanges();
            }
            catch
            {

            }
            friend_Re.sender1_id = x;
            friend_Re.sender = db.Users.Find(x);

            friend_Re.resever1_id = idi;
            friend_Re.resever = db.Users.Find(idi);
            
            db.friend_Res.Add(friend_Re);
            db.SaveChanges();

            return RedirectToAction("my_re");
        }
                
                //To Accept Your Request If it is Friend
        public ActionResult friendsRe()
        {
            int x = Convert.ToInt32(Session["Userid"]);
            List<friend_re> friend_Res = db.friend_Res.Where(
                m => m.sender1_id == x).ToList();
            return View(friend_Res);
        }
        public ActionResult myFreinds(int? id)
        {
            friend_re friend_Re = new friend_re();
            int x = Convert.ToInt32(Session["Userid"]);
            int idi = Convert.ToInt32(id);
            try
            {
                List<friend_re> g =
                     db.friend_Res.Where(
                         m => m.sender1_id == x && m.resever1_id == idi
                         ).ToList();
                db.friend_Res.RemoveRange(g);
                db.SaveChanges();
            }
            catch
            {

            }
            friend_Re.sender1_id = x;
            friend_Re.sender = db.Users.Find(x);

            friend_Re.resever1_id = idi;
            friend_Re.resever = db.Users.Find(idi);

            db.friend_Res.Add(friend_Re);
            db.SaveChanges();

            return RedirectToAction("friendsRe");
        }
                
                //Action Method To cancle Request
        public ActionResult cancle(int? id)
        {
            int x = Convert.ToInt32(Session["Userid"]);
            int idi = Convert.ToInt32(id);
            try
            {
                List<friend_re> g =
                   db.friend_Res.Where(
                       m => m.sender1_id == x && m.resever1_id == idi
                       ).ToList();
                db.friend_Res.RemoveRange(g);
                db.SaveChanges();

            }
            catch
            {

            }
            return RedirectToAction("my_re");
        }
                
                //Action Method To Search Person
        public ActionResult getpersons (FormCollection form)
        {
            int x = Convert.ToInt32(Session["Userid"]);
            string name = form["name"].ToString();
            List<User> users = db.Users.Where(m => m.FName == name).ToList();
            return View(users);
        }

                //To Edit Your Profile
        public ActionResult incom_requst()
        {
            int x = Convert.ToInt32(Session["Userid"]);
            List<friend_re> users =
                db.friend_Res.Where(m => m.resever1_id == x).ToList();
            return View();
        }
    }
}