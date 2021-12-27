using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using Marketing_Tool.Models;

namespace Marketing_Tool.Controllers
{
    public class AdminController : Controller
    {
 
        public ActionResult studentDetails()
        {
            Marketing_ToolsEntities mt = new Marketing_ToolsEntities();
           
            //**dashboard
            //var Remaining = mt.Student_tbl.Where(p => p.UserID != null).Count(me => me.Action == null || me.Response == "Not Confirmed");
            //ViewBag.data1 = Remaining;

            //DateTime date = DateTime.Now;
           
            //var TodayCon = mt.Student_tbl.Where(p => p.UserID != null).Count(me => me.Response == "Confirmed" && me.Date==date );
            //ViewBag.data2 = TodayCon;

            //var Pending = mt.Student_tbl.Where(p => p.UserID != null).Count(me => me.Response == "Not Confirmed");
            //ViewBag.data3 = Pending;
            //** end of dashboard

            //*** Dropdown list
            var items = mt.CategoryLists.ToList();
            if (items != null)
            {
                System.Diagnostics.Debug.WriteLine("Category called");
                ViewBag.data = items;
            }

            //***end of dropdown

            return View(mt.Student_tbl.ToList().Where(p => p.UserID != null && p.Action == null));
        }
        [HttpPost]
        public ActionResult studentDetails(String categoryName)
        {
            Marketing_ToolsEntities mt = new Marketing_ToolsEntities();

            //*** Dropdown list
            var items = mt.CategoryLists.ToList();
            if (items != null)
            {
                System.Diagnostics.Debug.WriteLine("Category called");
                ViewBag.data = items;
            }

            //***end of dropdown

            var cn = mt.Category_tbl.Where(x => x.CategoryID == categoryName).FirstOrDefault();
            var ww = cn.CategoryType;

            return View(mt.Student_tbl.ToList().Where(p => p.UserID != null && p.Category == ww && p.Action == null));
        }
        public ActionResult viewstudentDetails(String id)
        {
            using (Marketing_ToolsEntities mt = new Marketing_ToolsEntities())
            {
                // System.Diagnostics.Debug.WriteLine("M_viewstudentDetails called" );
                //System.Diagnostics.Debug.WriteLine(id);
                return View(mt.Student_tbl.Where(x => x.StudentID == id).FirstOrDefault());
            }
        }


        [HttpPost]
        public ActionResult viewstudentDetails(String id, Student_tbl student)
        {
            try
            {
                using (Marketing_ToolsEntities mt = new Marketing_ToolsEntities())
                {
                    var rec = mt.Student_tbl.Where(a => a.StudentID == id).FirstOrDefault();
                    rec.comment1 = student.comment1;
                    rec.comment2 = student.comment2;
                    rec.Action = student.Action;
                    rec.Response = student.Response;
                    DateTime now = DateTime.Now;
                    rec.Date = now;
                    rec.NextCallingDate = student.NextCallingDate;
                    mt.Entry(rec).State = EntityState.Modified;

                    mt.SaveChanges();
                }
                // TODO: Add update logic here
                //       System.Diagnostics.Debug.WriteLine("action save student called");
                return RedirectToAction("studentDetails");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                return View();
            }
        }

        public ActionResult AssignMarToCategory()
        {
            return View();
        }
       
    }
}
