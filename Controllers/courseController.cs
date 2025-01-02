using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using tryno2.Models;

namespace tryno2.Controllers
{
    public class courseController : Controller
    {
        Model1 db = new Model1(); 
        // GET: course
        public ActionResult search(string searchtext)
        {
            return View(db.Courses.Include(s=>s.Department).Where(x=>x.Name.Contains(searchtext)||x.Department.Name.Contains(searchtext)||searchtext==null).ToList());
        }

    }
}