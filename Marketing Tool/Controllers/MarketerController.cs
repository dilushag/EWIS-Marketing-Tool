using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Marketing_Tool.Models;

namespace Marketing_Tool.Controllers
{
    public class MarketerController : Controller
    {
      //  private Marketing_ToolEntities DbModel = new Marketing_ToolEntities();
          // GET: Marketer/M_studentDetails

        public ActionResult M_studentDetails()
        {
            Marketing_ToolsEntities mt = new Marketing_ToolsEntities();
            //**dashboard

            var Remaining = mt.Student_tbl.Where(p => p.UserID == "MT21000001").Count(me => me.Action == null  ||  me.Response  == "Not Confirmed" );
            ViewBag.data1 = Remaining;

            DateTime date = DateTime.Now;
            var TodayCon = mt.Student_tbl.Where(p => p.UserID == "MT21000001").Count(me => me.Response == "Confirmed" && me.Date == date);
            ViewBag.data2 = TodayCon;

            var Pending = mt.Student_tbl.Where(p => p.UserID == "MT21000001").Count(me => me.Response == "Not Confirmed");
            ViewBag.data3 = Pending;
            //** end of dashboard

            //*** Dropdown list
            var items = mt.CategoryLists.ToList();
            if (items != null)
            {
                System.Diagnostics.Debug.WriteLine("Category called");
                ViewBag.data = items;
            }

            //***end of dropdown

            //  List<Student_tbl> stu = mt.Student_tbl.ToList();
            //List<Student_user> stuMar = mt.Student_user.ToList();
            //var mutiTable = from st in stu
            //                where st.UserID == "MT21000001"
            //                select new  { Student_tbl = st };

            //ViewBag.CategortName = new SelectList(mt.Student_tbl.ToList(),"Category");
           // return View(stu.student_Details.ToList().Where(p => p.student.ToLower().StartsWith(search) || p.school.ToLower().StartsWith(search) || p.grade.ToLower().StartsWith(search) || p.subject.ToLower().StartsWith(search) || p.AL_OL.ToLower().StartsWith(search) || p.year_.ToLower().StartsWith(search)));

            return View(mt.Student_tbl.ToList().Where(p => p.UserID == "MT21000001" && p.Action == null));
        }
        [HttpPost]
        public ActionResult M_studentDetails(String categoryName)
        {
            // String Selcategory = form["categoryName"].ToString();
            Marketing_ToolsEntities mt = new Marketing_ToolsEntities();

            //**dashboard

            var Remaining = mt.Student_tbl.Where(p => p.UserID == "MT21000001").Count(me => me.Action == null || me.Response == "Not Confirmed" );
            ViewBag.data1 = Remaining;

            DateTime date = DateTime.Now;
            var TodayCon = mt.Student_tbl.Where(p => p.UserID == "MT21000001").Count(me => me.Response == "Confirmed" && me.Date == date);
            ViewBag.data2 = TodayCon;

            var Pending = mt.Student_tbl.Where(p => p.UserID == "MT21000001").Count(me => me.Response == "Not Confirmed");
            ViewBag.data3 = Pending;
            //** end of dashboard

            //*** Dropdown list
            var items = mt.CategoryLists.ToList();
            if (items != null)
            {
                System.Diagnostics.Debug.WriteLine("Category called");
                ViewBag.data = items;
            }

            //***end of dropdown

            //search
            var cn = mt.Category_tbl.Where(x => x.CategoryID == categoryName ).FirstOrDefault();
            var ww =cn.CategoryType;

          
            //   var catName = mt.Category_tbl.Where(a => a.CategoryID == categoryName).Select(b => new { b.CategoryType});
            // var students = mt.Student_tbl.Where(a => a.Category == ww);
           // List<Student_tbl> stu = mt.Student_tbl.ToList();
         //  List<Category_tbl> cat = mt.Category_tbl.ToList();

            //var catName =  from st in stu
            //               where st.Category == ww
            //               select new jointClass { studentDetails = st};
            return View(mt.Student_tbl.ToList().Where(p => p.UserID == "MT21000001" && p.Category==ww && p.Action==null));
        }

        public ActionResult M_viewstudentDetails(String id )
        {
            using (Marketing_ToolsEntities mt = new Marketing_ToolsEntities())
            {
                // System.Diagnostics.Debug.WriteLine("M_viewstudentDetails called" );
                //System.Diagnostics.Debug.WriteLine(id);
                return View(mt.Student_tbl.Where(x => x.StudentID == id).FirstOrDefault());
            }
        }
        [HttpPost]
        public ActionResult M_viewstudentDetails(String id, Student_tbl student)
        {    
            //update
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
                return RedirectToAction("M_studentDetails");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                return View();
            }
        }
        public ActionResult DashBoard()

        {
            // String Selcategory = form["categoryName"].ToString();
            Marketing_ToolsEntities mt = new Marketing_ToolsEntities();

            //**dashboard

            var confirmed = mt.Student_tbl.Where(p => p.UserID == "MT21000001").Count(me =>me.Response == "Confirmed");
            ViewBag.confirmed = confirmed;

            var UnAnswerd = mt.Student_tbl.Where(p => p.UserID == "MT21000001").Count(me => me.comment1 == "Not Answered");
            ViewBag.UnAnswerd = UnAnswerd;

            var Rejected = mt.Student_tbl.Where(p => p.UserID == "MT21000001").Count(me => me.Response == "Rejected");
            ViewBag.Rejected = Rejected;

            var Pending = mt.Student_tbl.Where(p => p.UserID == "MT21000001").Count(me => me.Response == "Not Confirmed");
            ViewBag.Pending = Pending;

            DateTime date = DateTime.Now;
            var TodayInquary = mt.Student_tbl.Where(p => p.UserID == "MT21000001").Count(me => me.NextCallingDate == date);
            ViewBag.TodayInquary = TodayInquary;

            //** end of dashboard
         
            return View();
        }


        public ActionResult ConfirmedStudents()
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

            return View(mt.Student_tbl.ToList().Where(p => p.UserID == "MT21000001" && p.Response == "Confirmed"));
        }
        [HttpPost]
        public ActionResult ConfirmedStudents(String categoryName) {

            Marketing_ToolsEntities mt = new Marketing_ToolsEntities();

            //*** Dropdown list
            var items = mt.CategoryLists.ToList();
            if (items != null)
            {
                System.Diagnostics.Debug.WriteLine("Category called");
                ViewBag.data = items;
            }

            //***end of dropdown

            //search
            var cn = mt.Category_tbl.Where(x => x.CategoryID == categoryName).FirstOrDefault();
            var ww = cn.CategoryType;
            return View(mt.Student_tbl.ToList().Where(p => p.UserID == "MT21000001" && p.Category == ww && p.Response == "Confirmed"));
        }

        public ActionResult EditConfStudentDetails(String id)
        {
            using (Marketing_ToolsEntities mt = new Marketing_ToolsEntities())
            {
                // System.Diagnostics.Debug.WriteLine("M_viewstudentDetails called" );
                //System.Diagnostics.Debug.WriteLine(id);
                return View(mt.Student_tbl.Where(x => x.StudentID == id).FirstOrDefault());

            }
        }
        [HttpPost]
        public ActionResult EditConfStudentDetails(String id, Student_tbl student)
        {
            //update
            try
            {
                using (Marketing_ToolsEntities mt = new Marketing_ToolsEntities())
                {
                    var rec = mt.Student_tbl.Where(a => a.StudentID == id).FirstOrDefault();
                    rec.StudentID = student.StudentID;
                    rec.StudentName = student.StudentName;
                    rec.StudentEmail = student.StudentEmail;
                    rec.NIC = student.NIC;
                    rec.Dob = student.Dob;
                    rec.Addresss = student.Addresss;
                    rec.Category = student.Category;
                    rec.ContactNumber = student.ContactNumber;
                    rec.comment1 = student.comment1;
                    rec.comment2 = student.comment2;
                    rec.Action = student.Action;
                    rec.Response = student.Response;
                    DateTime now = DateTime.Now;
                    rec.Date = now;
                    
                    mt.Entry(rec).State = EntityState.Modified;

                    mt.SaveChanges();
                }
                // TODO: Add update logic here
                //       System.Diagnostics.Debug.WriteLine("action save student called");
                return RedirectToAction("ConfirmedStudents");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                return View();
            }
        }

        public ActionResult DeleteConfStudentDetails(String id)
        {
            using (Marketing_ToolsEntities mt = new Marketing_ToolsEntities())
            {
                // System.Diagnostics.Debug.WriteLine("M_viewstudentDetails called" );
                //System.Diagnostics.Debug.WriteLine(id);
                return View(mt.Student_tbl.Where(x => x.StudentID == id).FirstOrDefault());

            }
        }
        [HttpPost]
        public ActionResult DeleteConfStudentDetails(String id, FormCollection collection)
        {
            try
            {
                //delete
                // TODO: Add delete logic here
                using (Marketing_ToolsEntities dbModels = new Marketing_ToolsEntities())
                {
                    Student_tbl user = dbModels.Student_tbl.Where(x => x.StudentID == id).FirstOrDefault();
                    dbModels.Student_tbl.Remove(user);
                    dbModels.SaveChanges();
                }
                return RedirectToAction("ConfirmedStudents");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Student_Reports()
        {
            return View();
        }

       

        
    }
}
