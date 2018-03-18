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
    public class ThreadsController : Controller
    {
        private ThreadRepasitory threadRepasitory = new ThreadRepasitory();

        // GET: Threads
        public async Task<ActionResult> Index(int id)
        {
            return View(await threadRepasitory.GetThreadsAsync(id));
        }

        // GET: Threads/Details/5
        public async Task<ActionResult> Details(int id)
        {
            Thread thread = await threadRepasitory.GetById(id);
            if (thread == null)
            {
                return HttpNotFound();
            }
            return View(thread);
        }

        // GET: Threads/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Threads/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,TextDescription,UserId,TopicId")] Thread thread)
        {
            if (ModelState.IsValid)
            {
                //threadRepasitory.CreateThreadAsync(thread.Name,thread.TextDescription,User.Identity.GetUserId()) ???????????
                return RedirectToAction("Index");
            }

            return View(thread);
        }

        // GET: Threads/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Thread thread = await threadRepasitory.GetById(id);
            if (thread == null)
            {
                return HttpNotFound();
            }
            return View(thread);
        }

        // POST: Threads/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,TextDescription,UserId,TopicId")] Thread thread)
        {
            if (ModelState.IsValid)
            {
                await  threadRepasitory.UpdateThread(thread);
                return RedirectToAction("Index");
            }
            return View(thread);
        }

        // GET: Threads/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            Thread thread = await threadRepasitory.GetById(id);
            if (thread == null)
            {
                return HttpNotFound();
            }
            return View(thread);
        }

        // POST: Threads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await  threadRepasitory.Remove(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                threadRepasitory.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
