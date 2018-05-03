using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Magazine.Models;

namespace Magazine.Controllers
{
    public class CurrentViolationsController : Controller
    {
        private MyDBEntities8 db = new MyDBEntities8();

        // GET: CurrentViolations
        public ActionResult Index()
        {
            var currentViolations = db.CurrentViolations.Include(c => c.Revision).Include(c => c.Violation);
            return View(currentViolations.ToList());
        }

        public ActionResult IndexPayFalse()
        {
            var currentViolations = db.CurrentViolations.Include(c => c.Revision).Include(c => c.Violation);
            return View(currentViolations.ToList());
        }
        public ActionResult IndexPayTrue()
        {
            var currentViolations = db.CurrentViolations.Include(c => c.Revision).Include(c => c.Violation);
            return View(currentViolations.ToList());
        }
        public ActionResult IndexDisposalFalse()
        {
            var currentViolations = db.CurrentViolations.Include(c => c.Revision).Include(c => c.Violation);
            return View(currentViolations.ToList());
        }
        public ActionResult IndexDisposalTrue()
        {
            var currentViolations = db.CurrentViolations.Include(c => c.Revision).Include(c => c.Violation);
            return View(currentViolations.ToList());
        }

        // GET: CurrentViolations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CurrentViolation currentViolation = db.CurrentViolations.Find(id);
            if (currentViolation == null)
            {
                return HttpNotFound();
            }
            return View(currentViolation);
        }

        // GET: CurrentViolations/Create
        public ActionResult Create()
        {
            ViewBag.RevisionID = new SelectList(db.Revisions, "RevisionID","RevisionID");
            
            ViewBag.ViolationID = new SelectList(db.Violations, "ViolationID", "Name");
            return View();
        }

        // POST: CurrentViolations/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CurrentViolationID,ViolationID,Responsible,UsingSanctions,DisposalCheck,PayCheck,DispTime,RevisionID")] CurrentViolation currentViolation)
        {
            if (ModelState.IsValid)
            {
                db.CurrentViolations.Add(currentViolation);
                db.SaveChanges();
                return RedirectToAction("Create", "CurrentViolations");
            }

            ViewBag.RevisionID = new SelectList(db.Revisions, "RevisionID", "RevisionDate", currentViolation.RevisionID);
            ViewBag.ViolationID = new SelectList(db.Violations, "ViolationID", "Name", currentViolation.ViolationID);
            return View(currentViolation);
        }

        // GET: CurrentViolations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CurrentViolation currentViolation = db.CurrentViolations.Find(id);
            if (currentViolation == null)
            {
                return HttpNotFound();
            }
            ViewBag.RevisionID = new SelectList(db.Revisions, "RevisionID", "RevisionID", currentViolation.RevisionID);
            ViewBag.ViolationID = new SelectList(db.Violations, "ViolationID", "Name", currentViolation.ViolationID);
            return View(currentViolation);
        }

        // POST: CurrentViolations/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CurrentViolationID,ViolationID,Responsible,UsingSanctions,DisposalCheck,PayCheck,DispTime,RevisionID")] CurrentViolation currentViolation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(currentViolation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RevisionID = new SelectList(db.Revisions, "RevisionID", "RevisionDate", currentViolation.RevisionID);
            ViewBag.ViolationID = new SelectList(db.Violations, "ViolationID", "Name", currentViolation.ViolationID);
            return View(currentViolation);
        }

        // GET: CurrentViolations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CurrentViolation currentViolation = db.CurrentViolations.Find(id);
            if (currentViolation == null)
            {
                return HttpNotFound();
            }
            return View(currentViolation);
        }

        // POST: CurrentViolations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CurrentViolation currentViolation = db.CurrentViolations.Find(id);
            db.CurrentViolations.Remove(currentViolation);
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
