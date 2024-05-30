using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Student_Management_system.Models;

namespace Student_Management_system.Controllers
{
    public class RegistrationController : Controller
    {
        private AQ_SchoolEntities db = new AQ_SchoolEntities();

        // GET: Registration
        public ActionResult Index()
        {
            var registrations = db.Registrations.Include(r => r.Batch).Include(r => r.Course);
            return View(registrations.ToList());
        }

        // GET: Registration/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Registration registration = db.Registrations.Find(id);
            if (registration == null)
            {
                return HttpNotFound();
            }
            return View(registration);
        }

        // GET: Registration/Create
        public ActionResult Create()
        {
            ViewBag.Batch_id = new SelectList(db.Batches, "id", "Batch1");
            ViewBag.Course_id = new SelectList(db.Courses, "id", "Course1");
            return View();
        }

        // POST: Registration/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,First_name,Last_name,NIC,Course_id,Batch_id,Tel_No")] Registration registration)
        {
            if (ModelState.IsValid)
            {
                db.Registrations.Add(registration);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Batch_id = new SelectList(db.Batches, "id", "Batch1", registration.Batch_id);
            ViewBag.Course_id = new SelectList(db.Courses, "id", "Course1", registration.Course_id);
            return View(registration);
        }

        // GET: Registration/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Registration registration = db.Registrations.Find(id);
            if (registration == null)
            {
                return HttpNotFound();
            }
            ViewBag.Batch_id = new SelectList(db.Batches, "id", "Batch1", registration.Batch_id);
            ViewBag.Course_id = new SelectList(db.Courses, "id", "Course1", registration.Course_id);
            return View(registration);
        }

        // POST: Registration/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,First_name,Last_name,NIC,Course_id,Batch_id,Tel_No")] Registration registration)
        {
            if (ModelState.IsValid)
            {
                db.Entry(registration).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Batch_id = new SelectList(db.Batches, "id", "Batch1", registration.Batch_id);
            ViewBag.Course_id = new SelectList(db.Courses, "id", "Course1", registration.Course_id);
            return View(registration);
        }

        // GET: Registration/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Registration registration = db.Registrations.Find(id);
            if (registration == null)
            {
                return HttpNotFound();
            }
            return View(registration);
        }

        // POST: Registration/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Registration registration = db.Registrations.Find(id);
            db.Registrations.Remove(registration);
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
