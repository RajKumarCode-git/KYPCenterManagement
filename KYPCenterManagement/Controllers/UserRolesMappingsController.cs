using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KYPCenterManagement.Models;

namespace KYPCenterManagement.Controllers
{
    [Authorize(Roles = "Adminstrator")]
    public class UserRolesMappingsController : Controller
    {
        private KYPCenterManagementEntities db = new KYPCenterManagementEntities();

        // GET: UserRolesMappings
        public ActionResult Index()
        {
            var userRolesMappings = db.UserRolesMappings.Include(u => u.Role).Include(u => u.Role1).Include(u => u.User);
            return View(userRolesMappings.ToList());
        }

        // GET: UserRolesMappings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserRolesMapping userRolesMapping = db.UserRolesMappings.Find(id);
            if (userRolesMapping == null)
            {
                return HttpNotFound();
            }
            return View(userRolesMapping);
        }

        // GET: UserRolesMappings/Create
        public ActionResult Create()
        {
            ViewBag.RoleId = new SelectList(db.Roles, "Id", "RoleName");
            ViewBag.RoleId = new SelectList(db.Roles, "Id", "RoleName");
            ViewBag.UserId = new SelectList(db.Users, "Id", "Username");
            return View();
        }

        // POST: UserRolesMappings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,RoleId")] UserRolesMapping userRolesMapping)
        {
            if (ModelState.IsValid)
            {
                db.UserRolesMappings.Add(userRolesMapping);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RoleId = new SelectList(db.Roles, "Id", "RoleName", userRolesMapping.RoleId);
            ViewBag.RoleId = new SelectList(db.Roles, "Id", "RoleName", userRolesMapping.RoleId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Username", userRolesMapping.UserId);
            return View(userRolesMapping);
        }

        // GET: UserRolesMappings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserRolesMapping userRolesMapping = db.UserRolesMappings.Find(id);
            if (userRolesMapping == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoleId = new SelectList(db.Roles, "Id", "RoleName", userRolesMapping.RoleId);
            ViewBag.RoleId = new SelectList(db.Roles, "Id", "RoleName", userRolesMapping.RoleId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Username", userRolesMapping.UserId);
            return View(userRolesMapping);
        }

        // POST: UserRolesMappings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,RoleId")] UserRolesMapping userRolesMapping)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userRolesMapping).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RoleId = new SelectList(db.Roles, "Id", "RoleName", userRolesMapping.RoleId);
            ViewBag.RoleId = new SelectList(db.Roles, "Id", "RoleName", userRolesMapping.RoleId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Username", userRolesMapping.UserId);
            return View(userRolesMapping);
        }

        // GET: UserRolesMappings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserRolesMapping userRolesMapping = db.UserRolesMappings.Find(id);
            if (userRolesMapping == null)
            {
                return HttpNotFound();
            }
            return View(userRolesMapping);
        }

        // POST: UserRolesMappings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserRolesMapping userRolesMapping = db.UserRolesMappings.Find(id);
            db.UserRolesMappings.Remove(userRolesMapping);
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
