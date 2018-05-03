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
    public class EnterprisesController : Controller
    {
        private MyDBEntities8 db = new MyDBEntities8();

        // GET: Enterprises
        public ActionResult Index()
        {
            return View(db.Enterprises.ToList());
        }


        // GET: Enterprises/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Enterprises/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EnterpriseID,PropForm,Name,BossLastName,BossFirstName,BossMiddleName,Address,PhoneNumber")] Enterprise enterprise)
        {
            if (ModelState.IsValid)
            {
                db.Enterprises.Add(enterprise);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(enterprise);
        }

        // GET: Enterprises/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enterprise enterprise = db.Enterprises.Find(id);
            if (enterprise == null)
            {
                return HttpNotFound();
            }
            return View(enterprise);
        }

        // POST: Enterprises/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EnterpriseID,PropForm,Name,BossLastName,BossFirstName,BossMiddleName,Address,PhoneNumber")] Enterprise enterprise)
        {
            if (ModelState.IsValid)
            {
                db.Entry(enterprise).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(enterprise);
        }

        // GET: Enterprises/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enterprise enterprise = db.Enterprises.Find(id);
            if (enterprise == null)
            {
                return HttpNotFound();
            }
            return View(enterprise);
        }

        // POST: Enterprises/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Enterprise enterprise = db.Enterprises.Find(id);
            db.Enterprises.Remove(enterprise);
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
