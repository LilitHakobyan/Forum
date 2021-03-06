﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Forum.Entity;
using Forum.Models;

namespace Forum.Controllers
{
    public class TopicsController : Controller
    {
        private TopicRepasitory topicRepositry = new TopicRepasitory();

        // GET: Topics
        public async Task<ActionResult> Index()
        { 
            return View(await topicRepositry.GetTopics());
        }

        // GET: Topics/Details/5
        public async Task<ActionResult> Details(int id)
        {
            
            Topic topic = await topicRepositry.GetById(id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            return View(topic);
        }

        // GET: Topics/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Topics/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name")] Topic topic)
        {
            if (ModelState.IsValid)
            {
                await topicRepositry.CreateTopicAsync(topic.Name);
                return RedirectToAction("Index");
            }

            return View(topic);
        }

        // GET: Topics/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Topic topic = await topicRepositry.GetById(id);//   Topics.FindAsync(id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            return View(topic);
        }

        // POST: Topics/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name")] Topic topic)
        {
            if (ModelState.IsValid)
            {
              //  topicRepositry.Entry(topic).State = EntityState.Modified;
                await topicRepositry.UpdateTopic(topic);
                return RedirectToAction("Index");
            }
            return View(topic);
        }

        // GET: Topics/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
           
            Topic topic = await topicRepositry.GetById(id);//Topics.FindAsync(id));
            if (topic == null)
            {
                return HttpNotFound();
            }
            return View(topic);
        }

        // POST: Topics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await  topicRepositry.Remove(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                topicRepositry.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
