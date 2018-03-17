using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Forum.Entity
{
    public class Topic
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Thread> TopicThreads { get; set; }

        public Topic()
        {
            TopicThreads=new List<Thread>();
        }
    }
}