using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Forum.Entity;
using Forum.Models;
using Microsoft.AspNet.Identity;

namespace Forum.Controllers
{
    public class PostsController : Controller
    {
        private PostRepasitory postRepasitory = new PostRepasitory();
        private static int threadId;
        // GET: Posts
        public async Task<ActionResult> Index(int id,string topic,string thread,string text)
        {
            ViewBag.TopicName = topic;
            ViewBag.ThreadName = thread;
            ViewBag.Text = text;
            threadId = id;
            return View(await postRepasitory.GetPostsAsync(threadId));
        }

        // GET: Posts/Details/5
        public async Task<ActionResult> Details(int id)
        {
            Post post = await postRepasitory.GetById(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // GET: Posts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Text,ThreadId,UserId")] Post post)
        {
            if (ModelState.IsValid)
            {
                await postRepasitory.CreatePostAsync(post.Text, threadId, Guid.Parse(User.Identity.GetUserId()),User.Identity.GetUserName());
                //return RedirectToAction("Index");
                return RedirectToRoute(new { controller = "Posts", action = "Index", id = threadId });
            }
            return View(post);
        }

        // GET: Posts/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Post post = await postRepasitory.GetById(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Text,ThreadId,UserId")] Post post)
        {
            if (ModelState.IsValid)
            {
                await postRepasitory.UpdatePost(post);
                return RedirectToRoute(new { controller = "Posts", action = "Index", id = threadId });
            }
            return View(post);
        }

        // GET: Posts/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            Post post = await postRepasitory.GetById(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await postRepasitory.Remove(id);
            return RedirectToRoute(new { controller = "Posts", action = "Index", id = threadId });

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                postRepasitory.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
