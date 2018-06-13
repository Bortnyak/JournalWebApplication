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
    public class RatingsController : Controller
    {
        private JournalModelDB db = new JournalModelDB();
        //GET: Ratings
        public async Task<ActionResult> Index(int? Id, int? groupId, int? hoursId)
        {
            ViewBag.Students = db.Students.Where(x => x.GroupId == groupId).ToArray();
            ViewBag.StudentsId = db.Students.Where(x => x.GroupId == groupId).Select(x => x.Id).ToArray();

            ViewBag.Date = db.Ratings.Where(x => x.HoursId == hoursId).Select(x => x.Date).Distinct().ToArray();
            var ratings = db.Ratings.Where(x => x.HoursId == hoursId).ToArray();
            ViewBag.GroupId = groupId;
            ViewBag.HoursId = hoursId;
            ViewBag.RatingType = db.Ratings.Where(x => x.HoursId == hoursId).Select(x => x.RatingTypes.RatingType).ToArray();
            ViewBag.Ratings = db.Ratings.Where(x => x.HoursId == hoursId).ToArray();
            ViewBag.Topic = db.Ratings.Where(x => x.HoursId == hoursId).Select(x => x.TopicOfLesson).Distinct().ToArray();
            return View();
        }
        //POST: Records --> db.Journal
        [HttpPost]
        public async Task<ActionResult> AddRecordsToJournal(Ratings[] ratings)
        {
            for(int i = 0; i < ratings.Length; i++)
            {
                if(ratings[i].Rating != null)
                {
                    db.Ratings.Add(ratings[i]);
                    db.SaveChanges();
                }             
            }     
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> AddRecordsToJournal(Ratings ratings)
        {
                if (ratings.Rating != null)
                {
                    db.Ratings.Add(ratings);
                    db.SaveChanges();
                }       
            return RedirectToAction("Index");
        }
        // GET: Ratings/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ratings ratings = await db.Ratings.FindAsync(id);
            if (ratings == null)
            {
                return HttpNotFound();
            }
            return View(ratings);
        }

        // GET: Ratings/Create
        public ActionResult Create()
        {
            ViewBag.HoursId = new SelectList(db.Hours, "Id", "Id");
            ViewBag.RatingType_Id = new SelectList(db.RatingTypes, "Id", "RatingType");
            ViewBag.Student_Id = new SelectList(db.Students, "Id", "FullName");
            ViewBag.Subject_Id = new SelectList(db.Subjects, "Id", "SubjectName");
            return View();
        }

        // POST: Ratings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 

        // POST: Ratings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Student_Id,Subject_Id,Group_Id,Rating,RatingType_Id,TopicOfLesson,Date,HoursId")] Ratings ratings)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ratings).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.HoursId = new SelectList(db.Hours, "Id", "Id", ratings.HoursId);
            ViewBag.RatingType_Id = new SelectList(db.RatingTypes, "Id", "RatingType", ratings.RatingType_Id);
            ViewBag.Student_Id = new SelectList(db.Students, "Id", "FullName", ratings.Student_Id);
           // ViewBag.Subject_Id = new SelectList(db.Subjects, "Id", "SubjectName", ratings.Subject_Id);
            return View(ratings);
        }

        // GET: Ratings/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ratings ratings = await db.Ratings.FindAsync(id);
            if (ratings == null)
            {
                return HttpNotFound();
            }
            return View(ratings);
        }

        // POST: Ratings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Ratings ratings = await db.Ratings.FindAsync(id);
            db.Ratings.Remove(ratings);
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
