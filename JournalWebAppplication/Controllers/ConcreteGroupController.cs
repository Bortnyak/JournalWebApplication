using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JournalWebAppplication.Models;

namespace JournalWebAppplication.Controllers
{
    public class ConcreteGroupController : Controller
    {
        private JournalModelDB db = new JournalModelDB();

        // GET: ConcreteGroup
        public async Task<ActionResult> Index(int? groupId)
        {
            var subjects = await db.Hours.Where(x => x.GroupId == groupId).ToListAsync();
            var groupNameFromDb = await db.Groups.Where(g => g.Id == groupId).Select(g => g.GroupName).ToArrayAsync();
            var groupIdFromDb = await db.Groups.Where(g => g.Id == groupId).Select(g => g.Id).ToArrayAsync();
            var ViewGroupId = groupIdFromDb[0];
            var students = await db.Students.Where(x => x.GroupId == groupId).Select(x => x.FullName).ToArrayAsync();

            ViewBag.GroupId = ViewGroupId;
            ViewBag.GroupName = groupNameFromDb[0];
            ViewBag.Students = students;

            return View(subjects);
        }

        // GET: ConcreteGroup/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hours hours = await db.Hours.FindAsync(id);
            if (hours == null)
            {
                return HttpNotFound();
            }
            return View(hours);
        }

        // GET: ConcreteGroup/Create
        public ActionResult Create()
        {
            ViewBag.GroupId = new SelectList(db.Groups, "Id", "GroupName");
            //ViewBag.KindOfOccupationId = new SelectList(db.KindOfOccupation, "Id", "KindOfOccupation1");
            ViewBag.SubjectId = new SelectList(db.Subjects, "Id", "SubjectName");
            return View();
        }

        // POST: ConcreteGroup/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,SubjectId,KindOfOccupationId,GroupId,NumberOfHours")] Hours hours)
        {
            if (ModelState.IsValid)
            {
                db.Hours.Add(hours);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.GroupId = new SelectList(db.Groups, "Id", "GroupName", hours.GroupId);
            //ViewBag.KindOfOccupationId = new SelectList(db.KindOfOccupation, "Id", "KindOfOccupation1", hours.KindOfOccupationId);
            ViewBag.SubjectId = new SelectList(db.Subjects, "Id", "SubjectName", hours.SubjectId);
            return View(hours);
        }

        // GET: ConcreteGroup/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hours hours = await db.Hours.FindAsync(id);
            if (hours == null)
            {
                return HttpNotFound();
            }
            ViewBag.GroupId = new SelectList(db.Groups, "Id", "GroupName", hours.GroupId);
           // ViewBag.KindOfOccupationId = new SelectList(db.KindOfOccupation, "Id", "KindOfOccupation1", hours.KindOfOccupationId);
            ViewBag.SubjectId = new SelectList(db.Subjects, "Id", "SubjectName", hours.SubjectId);
            return View(hours);
        }

        // POST: ConcreteGroup/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,SubjectId,KindOfOccupationId,GroupId,NumberOfHours")] Hours hours)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hours).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.GroupId = new SelectList(db.Groups, "Id", "GroupName", hours.GroupId);
           // ViewBag.KindOfOccupationId = new SelectList(db.KindOfOccupation, "Id", "KindOfOccupation1", hours.KindOfOccupationId);
            ViewBag.SubjectId = new SelectList(db.Subjects, "Id", "SubjectName", hours.SubjectId);
            return View(hours);
        }

        // GET: ConcreteGroup/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hours hours = await db.Hours.FindAsync(id);
            if (hours == null)
            {
                return HttpNotFound();
            }
            return View(hours);
        }

        // POST: ConcreteGroup/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Hours hours = await db.Hours.FindAsync(id);
            db.Hours.Remove(hours);
            await db.SaveChangesAsync();
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
