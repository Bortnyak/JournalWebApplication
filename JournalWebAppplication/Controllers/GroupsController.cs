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
    public class GroupsController : Controller
    {
        private JournalModelDB db = new JournalModelDB();

        // GET: Groups
        public async Task<ActionResult> Index(int? Id)
        {
            var groups = db.Groups.Include(g => g.Pulpit).Where(g => g.PulpitId == Id);
            var pulpitName = db.Pulpit.Where(k => k.Id == Id).Select(p => p.PulpitName).ToArray();
            // db.Pulpit.Include(g => g.PulpitName).Where(g => g.Id == Id);
            ViewBag.PulpitName = pulpitName[0];
            return View(await groups.ToListAsync());
        }

        // GET: Groups/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Groups groups = await db.Groups.FindAsync(id);
            if (groups == null)
            {
                return HttpNotFound();
            }
            return View(groups);
        }

        // GET: Groups/Create
        public ActionResult Create()
        {
            ViewBag.Id = new SelectList(db.Pulpit, "Id", "PulpitName");
            return View();
        }

        // POST: Groups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,GroupName,Specialization,PulpitId")] Groups groups)
        {
            if (ModelState.IsValid)
            {
                db.Groups.Add(groups);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Id = new SelectList(db.Pulpit, "Id", "PulpitName", groups.Id);
            return View(groups);
        }

        // GET: Groups/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Groups groups = await db.Groups.FindAsync(id);
            if (groups == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id = new SelectList(db.Pulpit, "Id", "PulpitName", groups.Id);
            return View(groups);
        }

        // POST: Groups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,GroupName,Specialization,PulpitId")] Groups groups)
        {
            if (ModelState.IsValid)
            {
                db.Entry(groups).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Id = new SelectList(db.Pulpit, "Id", "PulpitName", groups.Id);
            return View(groups);
        }

        // GET: Groups/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Groups groups = await db.Groups.FindAsync(id);
            if (groups == null)
            {
                return HttpNotFound();
            }
            return View(groups);
        }

        // POST: Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Groups groups = await db.Groups.FindAsync(id);
            db.Groups.Remove(groups);
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
