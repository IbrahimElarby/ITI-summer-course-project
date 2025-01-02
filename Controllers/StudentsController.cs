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
    public class StudentsController : Controller
    {
        private Model1 db = new Model1();

        //[GET: Students]
        public ActionResult home(string password, string email,int? id)
        {
            password = Session["Password"].ToString();
            email = Session["Email"].ToString();
            id =int.Parse( $"{Session["ID"]}");
             //db.Courses.Include(s => s.Students).Where(c=>c.Take.St_id==id);
            
            return View(db.Takes.Include(s=>s.Course).Where(c => c.St_id == id));
        }
        public ActionResult Settings(string password)
        {
            password = Session["Password"].ToString();
            if (password== null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.FirstOrDefault(q => q.Password == password);
            if (student == null)
            {
                return HttpNotFound();
            }
           
            return View(student);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Settings( Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("index");
            }
           
            return View(student);
        }

        public ActionResult Index(string password, string email)
        {

            password = Session["Password"].ToString();
            email = Session["Email"].ToString();
            Student std = db.Students.FirstOrDefault(ss => ss.Email == email && ss.Password==password);
            
            //var students = db.Students.Include(s => s.Courses);
            return View(std);
        }
        public ActionResult search(string searchtext)
        {
            return View(db.Courses.Include(s => s.Department).Where(x => x.Name.Contains(searchtext) || x.Department.Name.Contains(searchtext) || searchtext == null).ToList());
        }
        public ActionResult dep1(int id)
        {
            var user = (from userlist in db.Departments
                        where userlist.ID == id
                        select new
                        {
                            userlist.Name,
                         
                        }).ToList();
            Session["dname"] = user.FirstOrDefault().Name.ToString();

            //Course std = db.Courses.Find(id);
            return View(db.Courses.Include(s=>s.Department).Where(x=>x.Department.ID==id));

        }
    
        public ActionResult Register()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Student s)
        {
            //Student student = db.Students.FirstOrDefault(w=>w.Email==s.Email );
            //if (student.Email == s.Email)
            //{
            //    return View(s);
            //}
            //else
            //{
                if (ModelState.IsValid)
                {
                    db.Students.Add(s);
                    db.SaveChanges();
                var user = (from userlist in db.Students
                            where userlist.Email == s.Email && userlist.Password == s.Password
                            select new
                            {
                                userlist.Password,
                                userlist.Email,
                                userlist.ID,
                                userlist.Fname
                            }).ToList();
                if (user.FirstOrDefault() == null)
                {
                    return View(s);
                }
                else
                {
                    Session["Email"] = user.FirstOrDefault().Email;
                    Session["Password"] = user.FirstOrDefault().Password;
                    Session["ID"] = user.FirstOrDefault().ID;
                    Session["Name"] = user.FirstOrDefault().Fname.ToString();
                    return RedirectToAction("home");
                }
            }

              return View(s);
            //}
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Student student)
        {


            var user = (from userlist in db.Students
                        where userlist.Email == student.Email && userlist.Password == student.Password
                        select new
                        {
                            userlist.Password,
                            userlist.Email,
                            userlist.ID,
                            userlist.Fname
                        }).ToList();
            if (user.FirstOrDefault() == null)
            {
                return View(student);
            }
            else
            {
                Session["Email"] = user.FirstOrDefault().Email;
                 Session["Password"] = user.FirstOrDefault().Password;
                Session["ID"] = user.FirstOrDefault().ID;
                Session["Name"] = user.FirstOrDefault().Fname.ToString();
                return RedirectToAction("home");
            }

        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }
        // GET: Students/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            ViewBag.course_id = new SelectList(db.Courses, "ID", "Name");
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Take student )
        {
            student.St_id = int.Parse($"{Session["ID"]}");
            if (ModelState.IsValid)
            {
                db.Takes.Add(student);
                db.SaveChanges();
                return RedirectToAction("Home");
            }

            ViewBag.course_id = new SelectList(db.Courses,"ID","Name");
            return View(student);
        }

        // GET: Students/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            ViewBag.dept_id = new SelectList(db.Departments, "ID", "Name");
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.dept_id = new SelectList(db.Departments, "ID", "Name");
            return View(student);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Take take = db.Takes.Find(id);
            if (take == null)
            {
                return HttpNotFound();
            }
            return View(take);
        }

        // POST: Takes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Take take = db.Takes.Find(id);
            db.Takes.Remove(take);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
