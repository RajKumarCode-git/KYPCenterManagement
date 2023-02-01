using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KYPCenterManagement.Models;
using System.IO;

namespace KYPCenterManagement.Controllers
{
    [Authorize]
    public class StudentsController : Controller
    {
        private KYPCenterManagementEntities db = new KYPCenterManagementEntities();

        // GET: Students
        public ActionResult Index(string search="",int PageNo=1)
        {
            ViewBag.search = search;
            List<Student> students = db.Students.Where(s => s.StudentName.Contains(search)).Include(s => s.Batch).Include(s => s.Center).ToList();
            int NoOfRecordsPerPage = 5;
            int NoOfPages = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(students.Count) / Convert.ToDouble(NoOfRecordsPerPage)));
            int NoOfRecordToSkip = (PageNo - 1) * NoOfRecordsPerPage;
            ViewBag.PageNo = PageNo;
            ViewBag.NoOfPages = NoOfPages;
            students = students.Skip(NoOfRecordToSkip).Take(NoOfRecordsPerPage).ToList();

            return View(students);
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
            ViewBag.BatchID = new SelectList(db.Batches, "Id", "BatchName");
            ViewBag.CenterID = new SelectList(db.Centers, "Id", "CenterName");
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,StudentName,StudentGender,StudentAge,StudentAddress,MobileNo,AadharNo,RegistationNo,FatherName,PaidAmount,DueAmount,BatchID,CenterID")] Student student)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Students.Add(student);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.BatchID = new SelectList(db.Batches, "Id", "BatchName", student.BatchID);
        //    ViewBag.CenterID = new SelectList(db.Centers, "Id", "CenterName", student.CenterID);
        //    return View(student);
        //}
        public ActionResult Create(Student student, HttpPostedFileBase file)
        {
            string fileName = (file != null) ? file.FileName : ((student.FilePath) != null) ? student.FilePath.Replace("~/Images/", "") : "NoImage.png";
            string path = Path.Combine(Server.MapPath("~/Images"), Path.GetFileName(fileName));
            if (file != null)
            {
                file.SaveAs(path);
            }
            if (ModelState.IsValid)
            {
                db.Students.Add(new Student
                {
                    Id = student.Id,
                    StudentName = student.StudentName,
                    StudentGender = student.StudentGender,
                    StudentAge = student.StudentAge,
                    StudentAddress = student.StudentAddress,
                    MobileNo = student.MobileNo,
                    AadharNo = student.AadharNo,
                    RegistationNo = student.RegistationNo,
                    FatherName = student.FatherName,
                    PaidAmount = student.PaidAmount,
                    DueAmount = student.DueAmount,
                    BatchID = student.BatchID,
                    CenterID = student.CenterID,
                    FilePath = "~/Images/" + fileName
                });
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BatchID = new SelectList(db.Batches, "Id", "BatchName", student.BatchID);
            ViewBag.CenterID = new SelectList(db.Centers, "Id", "CenterName", student.CenterID);
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
            ViewBag.BatchID = new SelectList(db.Batches, "Id", "BatchName", student.BatchID);
            ViewBag.CenterID = new SelectList(db.Centers, "Id", "CenterName", student.CenterID);
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Student student, HttpPostedFileBase file)
        {
            string fileName = (file != null) ? file.FileName : ((student.FilePath) != null) ? student.FilePath.Replace("~/Images/","") : "NoImage.png";
            string path = Path.Combine(Server.MapPath("~/Images"), Path.GetFileName(fileName));
            if(file != null)
            {
                file.SaveAs(path);
            }
            if (ModelState.IsValid)
            {
                db.Entry(new Student
                {
                    Id = student.Id,
                    StudentName = student.StudentName,
                    StudentGender = student.StudentGender,
                    StudentAge = student.StudentAge,
                    StudentAddress = student.StudentAddress,
                    MobileNo = student.MobileNo,
                    AadharNo = student.AadharNo,
                    RegistationNo = student.RegistationNo,
                    FatherName = student.FatherName,
                    PaidAmount = student.PaidAmount,
                    DueAmount = student.DueAmount,
                    BatchID = student.BatchID,
                    CenterID = student.CenterID,
                    FilePath = "~/Images/" + fileName
                }).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BatchID = new SelectList(db.Batches, "Id", "BatchName", student.BatchID);
            ViewBag.CenterID = new SelectList(db.Centers, "Id", "CenterName", student.CenterID);
            return View(student);
        }

        // GET: Students/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
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
