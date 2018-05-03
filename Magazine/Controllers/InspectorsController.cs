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
    public class InspectorsController : Controller
    {
        private MyDBEntities8 db = new MyDBEntities8();

        // GET: Inspectors
        public ActionResult Index()
        {
            var inspectors = db.Inspectors.Include(i => i.Division);
            return View(inspectors.ToList());
        }

        // GET: Inspectors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inspector inspector = db.Inspectors.Find(id);
            if (inspector == null)
            {
                return HttpNotFound();
            }
            return View(inspector);
        }

        //GET : Choise best Insp
        public ActionResult Best()
        {
            var inspectors = db.Inspectors.Include(i => i.Revisions);
            return View(inspectors.ToList());
        }


        // GET: Inspectors/Create
        public ActionResult Create()
        {
            ViewBag.DivisionID = new SelectList(db.Divisions, "DivisionID", "Name");
            return View();
        }

        // POST: Inspectors/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "InspectorID,DivisionID,LastName,FirstName,MiddleName")] Inspector inspector)
        {
            if (ModelState.IsValid)
            {
                db.Inspectors.Add(inspector);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DivisionID = new SelectList(db.Divisions, "DivisionID", "Name", inspector.DivisionID);
            return View(inspector);
        }

        // GET: Inspectors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inspector inspector = db.Inspectors.Find(id);
            if (inspector == null)
            {
                return HttpNotFound();
            }
            ViewBag.DivisionID = new SelectList(db.Divisions, "DivisionID", "Name", inspector.DivisionID);
            return View(inspector);
        }

        // POST: Inspectors/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "InspectorID,DivisionID,LastName,FirstName,MiddleName")] Inspector inspector)
        {
            if (ModelState.IsValid)
            {
                db.Entry(inspector).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DivisionID = new SelectList(db.Divisions, "DivisionID", "Name", inspector.DivisionID);
            return View(inspector);
        }

        // GET: Inspectors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inspector inspector = db.Inspectors.Find(id);
            if (inspector == null)
            {
                return HttpNotFound();
            }
            return View(inspector);
        }

        // POST: Inspectors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Inspector inspector = db.Inspectors.Find(id);
            db.Inspectors.Remove(inspector);
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
