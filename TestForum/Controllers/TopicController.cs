using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TestForum.Models;

namespace TestForum.Controllers
{
    public class TopicController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [AllowAnonymous]
        public async Task<ActionResult> Index()
        { 
            return View(await db.Topics.ToListAsync());
        }

        [AllowAnonymous]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TopicModel topicModel = await db.Topics
                .Include(a => a.Posts)
                .Where(a => a.Id == id)
                .FirstOrDefaultAsync();
  
            if (topicModel == null)
            {
                return HttpNotFound();
            }
            return View(topicModel);
        }

        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: TopicModels/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(TopicViewCreate topicModel)
        {
            if (ModelState.IsValid)
            {
                TopicModel model = new TopicModel();
                model.Title = topicModel.Title;
                model.AuthorName = User.Identity.Name;
                model.CreatedDate = DateTime.Now;
                model.AuthorId = User.Identity.GetUserId();
                db.Topics.Add(model);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(topicModel);
        }

        // GET: TopicModels/Edit/5
        [Authorize]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TopicModel topicModel = await db.Topics.FindAsync(id);
            if (topicModel == null)
            {
                return HttpNotFound();
            }
            return View(topicModel);
        }

        // POST: TopicModels/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> Edit(TopicModel topicModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(topicModel).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(topicModel);
        }

        // GET: TopicModels/Delete/5
        [Authorize]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TopicModel topicModel = await db.Topics.FindAsync(id);
            if (topicModel == null)
            {
                return HttpNotFound();
            }
            return View(topicModel);
        }

        // POST: TopicModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            TopicModel topicModel = await db.Topics.FindAsync(id);
            db.Topics.Remove(topicModel);
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
