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
    public class RevisionsController : Controller
    {
        private MyDBEntities8 db = new MyDBEntities8();

        // GET: Revisions
        public ActionResult Index()
        {
            var revisions = db.Revisions.Include(r => r.Enterprise).Include(r => r.Inspector);
            return View(revisions.ToList());
        }

        // GET: Revisions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Revision revision = db.Revisions.Find(id);
            if (revision == null)
            {
                return HttpNotFound();
            }
            return View(revision);
        }

        // GET: Revisions/Create
        public ActionResult Create()
        {
            ViewBag.EnterpriseID = new SelectList(db.Enterprises, "EnterpriseID", "Name");
            ViewBag.InspectorID = new SelectList(db.Inspectors, "InspectorID", "LastName");
            return View();
        }

        // POST: Revisions/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RevisionID,EnterpriseID,InspectorID,RevisionDate")] Revision revision)
        {
            if (ModelState.IsValid)
            {
                db.Revisions.Add(revision);
                db.SaveChanges();
                return RedirectToAction("Index","Revisions");
            }

            ViewBag.EnterpriseID = new SelectList(db.Enterprises, "EnterpriseID", "Name", revision.EnterpriseID);
            ViewBag.InspectorID = new SelectList(db.Inspectors, "InspectorID", "LastName", revision.InspectorID);
            return View(revision);
        }

        // GET: Revisions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Revision revision = db.Revisions.Find(id);
            if (revision == null)
            {
                return HttpNotFound();
            }
            ViewBag.EnterpriseID = new SelectList(db.Enterprises, "EnterpriseID", "Name", revision.EnterpriseID);
            ViewBag.InspectorID = new SelectList(db.Inspectors, "InspectorID", "LastName", revision.InspectorID);
            return View(revision);
        }

        // POST: Revisions/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RevisionID,EnterpriseID,InspectorID,RevisionDate")] Revision revision)
        {
            if (ModelState.IsValid)
            {
                db.Entry(revision).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EnterpriseID = new SelectList(db.Enterprises, "EnterpriseID", "Name", revision.EnterpriseID);
            ViewBag.InspectorID = new SelectList(db.Inspectors, "InspectorID", "LastName", revision.InspectorID);
            return View(revision);
        }

        // GET: Revisions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Revision revision = db.Revisions.Find(id);
            if (revision == null)
            {
                return HttpNotFound();
            }
            return View(revision);
        }

        // POST: Revisions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Revision revision = db.Revisions.Find(id);
            db.Revisions.Remove(revision);
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
