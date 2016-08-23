using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectCSharp.Models;
using System.Data.Entity;

namespace ProjectCSharp.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        //GET:Posts
        public ActionResult Index()
        {
            var posts = db.Posts.Include(p => p.Author).OrderByDescending(p => p.Date).Take(3);
            return View(posts.ToList());
        }

        
    }
}