using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TestForum.Models;

namespace TestForum.Controllers
{
    public class PostController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
       
        // GET: Post/Create
        [Authorize]
        public async Task<ActionResult> Create(int? id)
        {
            var topic = await context.Topics.FindAsync(id);
            var model = new PostNewModel
            {
                TopicId = topic.Id
            };
            return View(model);
        }

        // POST: Post/Create
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Create(PostNewModel model)
        {
            TopicModel topic = context.Topics.Find(model.TopicId);
            try
            {
                PostModel newModel = new PostModel();
                newModel.Content = model.Content;
                newModel.AuthorName = User.Identity.Name;
                newModel.CreatedDate = DateTime.Now;
                newModel.TopicId = topic.Id;
                newModel.Topic = context.Topics.Find(model.TopicId);
                context.Posts.Add(newModel);
                await context.SaveChangesAsync();
                topic.Posts.Add(newModel);
                return RedirectToAction("Details", "Topic", new { id = model.TopicId });
            }
            catch
            {
                return View(model);
            }
        }

        [Authorize]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PostModel model = await context.Posts.FindAsync(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Edit(PostModel postModel)
        {
            if (User.Identity.Name == postModel.AuthorName)
            {
                if (ModelState.IsValid)
                {
                    context.Entry(postModel).State = EntityState.Modified;
                    await context.SaveChangesAsync();
                    return RedirectToAction("Details", "Topic", new { id = postModel.TopicId });
                }
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(postModel);
        }

        [Authorize]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PostModel model = await context.Posts.FindAsync(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            PostModel model = await context.Posts.FindAsync(id);
            if (User.Identity.Name == model.AuthorName || User.IsInRole("Admin"))
            {
                context.Posts.Remove(model);
                await context.SaveChangesAsync();
                return RedirectToAction("Details", "Topic", new { id = model.TopicId });
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }
    }
}
