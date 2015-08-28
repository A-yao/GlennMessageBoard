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
    public class ARTICLEController : Controller
    {
        private MVCTESTEntities db = new MVCTESTEntities();

        // GET: ARTICLE
        public ActionResult Index(int? FORUM_ID)
        {
            IQueryable<ARTICLE> objs;
            if (FORUM_ID == null)
            {
                // Get: /Article
                objs = db.ARTICLE.Include(a => a.Forum);
            }
            else
            {
                // Get: /Article?forumID=12
                objs = db.ARTICLE.Where(x => x.FORUM_ID == FORUM_ID).OrderBy(x => x.BUD_DTM);
                ViewData["ForumDetailModel"] = db.Forum.Find(FORUM_ID);
            }
            return View(objs.ToList());
        }
        // GET: ARTICLE/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ARTICLE aRTICLE = db.ARTICLE.Find(id);
            if (aRTICLE == null)
            {
                return HttpNotFound();
            }
            return View(aRTICLE);
        }

        // GET: ARTICLE/Create
        public ActionResult Create(int? FORUM_ID)
        {
            ViewData["FORUM_ID"] = FORUM_ID;
            return View();
        }

        // POST: ARTICLE/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection frm)
        {
            ARTICLE obj = new ARTICLE();
            obj.BUD_DTM = DateTime.Now;
            obj.UPD_DTM = DateTime.Now;
            if (TryUpdateModel<ARTICLE>(obj, "", frm.AllKeys, new string[] { }))
            {
                db.ARTICLE.Add(obj);
                db.SaveChanges();
                return RedirectToAction("Details", "Forum", new { id = obj.FORUM_ID });
            }
            return View(obj);
        }

        // GET: ARTICLE/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ARTICLE aRTICLE = db.ARTICLE.Find(id);
            if (aRTICLE == null)
            {
                return HttpNotFound();
            }
            ViewBag.FORUM_ID = new SelectList(db.Forum, "ID", "TITLE", aRTICLE.FORUM_ID);
            return View(aRTICLE);
        }

        // POST: ARTICLE/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, FormCollection frm)
        {
            var obj = db.ARTICLE.Find(id);
            obj.UPD_DTM = DateTime.Now;
            if (TryUpdateModel<ARTICLE>(obj, "", frm.AllKeys, new string[] { "BUD_DTM" }))
            {
                db.SaveChanges();
                return RedirectToAction("Details", "Forum", new { id = obj.FORUM_ID });
            }
            return View(obj);
        }

        // GET: ARTICLE/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ARTICLE aRTICLE = db.ARTICLE.Find(id);
            if (aRTICLE == null)
            {
                return HttpNotFound();
            }
            return View(aRTICLE);
        }

        // POST: ARTICLE/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ARTICLE aRTICLE = db.ARTICLE.Find(id);
            var oid = aRTICLE.FORUM_ID;
            db.ARTICLE.Remove(aRTICLE);
            db.SaveChanges();
            return RedirectToAction("Details", "Forum", new { id = oid });
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
