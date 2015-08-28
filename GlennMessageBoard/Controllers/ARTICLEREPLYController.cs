using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GlennMessageBoard.Models;

namespace GlennMessageBoard.Controllers
{
    public class ARTICLEREPLYController : Controller
    {
        private MVCTESTEntities db = new MVCTESTEntities();

        // GET: ARTICLEREPLY
        public ActionResult Index()
        {
            var aRTICLEREPLY = db.ARTICLEREPLY.Include(a => a.ARTICLE);
            return View(aRTICLEREPLY.ToList());
        }

        // GET: ARTICLEREPLY/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ARTICLEREPLY aRTICLEREPLY = db.ARTICLEREPLY.Find(id);
            if (aRTICLEREPLY == null)
            {
                return HttpNotFound();
            }
            return View(aRTICLEREPLY);
        }

        // GET: ARTICLEREPLY/Create
        public ActionResult Create(int? ARTICLE_ID)
        {
            ViewData["ARTICLE_ID"] = ARTICLE_ID;
            return View();
        }

        // POST: ARTICLEREPLY/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection frm)
        {

            ARTICLEREPLY obj = new ARTICLEREPLY();
            obj.BUD_DTM = DateTime.Now;
            if (TryUpdateModel<ARTICLEREPLY>(obj, "", frm.AllKeys, new string[] { "" }))
            {
                db.ARTICLEREPLY.Add(obj);
                db.SaveChanges();
                return RedirectToAction("details", "article", new { ID = obj.ARTICLE_ID });
            }
            return View(obj);
        }

        // GET: ARTICLEREPLY/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ARTICLEREPLY aRTICLEREPLY = db.ARTICLEREPLY.Find(id);
            if (aRTICLEREPLY == null)
            {
                return HttpNotFound();
            }
            ViewBag.ARTICLE_ID = new SelectList(db.ARTICLE, "ID", "TITLE", aRTICLEREPLY.ARTICLE_ID);
            return View(aRTICLEREPLY);
        }

        // POST: ARTICLEREPLY/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, FormCollection frm)
        {
            var obj = db.ARTICLEREPLY.Find(id);
            if (TryUpdateModel<ARTICLEREPLY>(obj, "", frm.AllKeys, new string[] { }))
            {
                db.SaveChanges();
                return RedirectToAction("details", "article", new { ID = obj.ARTICLE_ID });
            }
            return View(obj);
        }

        // GET: ARTICLEREPLY/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ARTICLEREPLY aRTICLEREPLY = db.ARTICLEREPLY.Find(id);
            if (aRTICLEREPLY == null)
            {
                return HttpNotFound();
            }
            return View(aRTICLEREPLY);
        }

        // POST: ARTICLEREPLY/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ARTICLEREPLY aRTICLEREPLY = db.ARTICLEREPLY.Find(id);
            var OID = aRTICLEREPLY.ARTICLE_ID;
            db.ARTICLEREPLY.Remove(aRTICLEREPLY);
            db.SaveChanges();
            return RedirectToAction("details", "article", new { ID = OID });
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
