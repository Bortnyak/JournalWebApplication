using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JournalWebAppplication.Models;

namespace JournalWebAppplication.Controllers
{
    public class PulpitsController : Controller
    {
        private JournalModelDB db = new JournalModelDB();

        // GET: Pulpits
        public ActionResult Index(int? Id)
        {
            var pulpit = db.Pulpit.Include(p => p.Faculty).Include(p => p.Groups).Where(p => p.FacultyId == Id);
            var facultyName = db.Faculty.Where(k => k.Id == Id).Select(p => p.Name).ToArray();
            ViewBag.FacultyName = facultyName[0];
            return View(pulpit);
        }

        // GET: Pulpits/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pulpit pulpit = db.Pulpit.Find(id);
            if (pulpit == null)
            {
                return HttpNotFound();
            }
            return View(pulpit);
        }

        // GET: Pulpits/Create
        public ActionResult Create()
        {
            ViewBag.FacultyId = new SelectList(db.Faculty, "Id", "Name");
            ViewBag.Id = new SelectList(db.Groups, "Id", "GroupName");
            return View();
        }

        // POST: Pulpits/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PulpitName,FacultyId")] Pulpit pulpit)
        {
            if (ModelState.IsValid)
            {
                db.Pulpit.Add(pulpit);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FacultyId = new SelectList(db.Faculty, "Id", "Name", pulpit.FacultyId);
            ViewBag.Id = new SelectList(db.Groups, "Id", "GroupName", pulpit.Id);
            return View(pulpit);
        }

        // GET: Pulpits/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pulpit pulpit = db.Pulpit.Find(id);
            if (pulpit == null)
            {
                return HttpNotFound();
            }
            ViewBag.FacultyId = new SelectList(db.Faculty, "Id", "Name", pulpit.FacultyId);
            ViewBag.Id = new SelectList(db.Groups, "Id", "GroupName", pulpit.Id);
            return View(pulpit);
        }

        // POST: Pulpits/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PulpitName,FacultyId")] Pulpit pulpit)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pulpit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FacultyId = new SelectList(db.Faculty, "Id", "Name", pulpit.FacultyId);
            ViewBag.Id = new SelectList(db.Groups, "Id", "GroupName", pulpit.Id);
            return View(pulpit);
        }

        // GET: Pulpits/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pulpit pulpit = db.Pulpit.Find(id);
            if (pulpit == null)
            {
                return HttpNotFound();
            }
            return View(pulpit);
        }

        // POST: Pulpits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pulpit pulpit = db.Pulpit.Find(id);
            db.Pulpit.Remove(pulpit);
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
