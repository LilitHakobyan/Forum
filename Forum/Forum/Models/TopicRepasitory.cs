using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Forum.Entity;

namespace Forum.Models
{
    public class TopicRepasitory 
    {
        private Repasitory _repo;

        public TopicRepasitory(Repasitory repo)
        {
            if (repo == null)
                throw new ArgumentNullException("repo");
        }
        public IEnumerable<Topic> GetTopics()
        {
            List<Topic> topics=new List<Topic>();
            using (var cmd = _repo.CreateCommand())
            {
                cmd.CommandText = "SELECT Id,Name FROM Topics";
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        topics.Add(new Topic()
                        {
                            Id=(int)reader["Id"],
                            Name = (string)reader["Name"]
                        });
                    }
                }
            }
            return topics;
        }
        public Topic Get(int id)
        {
            string name="";
            using (var cmd = _repo.CreateCommand())
            {
                cmd.CommandText = "SELECT Name FROM Topics WHERE Id = @id";
                cmd.Parameters.AddWithValue("id", id);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        name = (string) reader["Name"];
                    }
                }
            }
            return new Topic() {Id = id, Name = name,TopicThreads =GetTopicThreads(id) };
        }

        public List<Thread> GetTopicThreads(int TopicId)
        {
              List<Thread> threads=new List<Thread>();
            using (var cmd = _repo.CreateCommand())
            {
                cmd.CommandText = "Select Id,Name,TextDescription from Threads WHERE Threads.TopicsId=@id";
                cmd.Parameters.AddWithValue("id", TopicId);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        threads.Add(new Thread()
                        {
                            Id = (int)reader[0],
                            Name = (string)reader[1],
                            TextDescription = (string)reader[2]
                        });
                    }
                }
            }
            return threads;
        }

        public void CreateTopic(string Name)
        {
            using (var cmd = _repo.CreateCommand())
            {
                cmd.CommandText = $"Insert into Person(Name) Values('test{Name}', 10)";
                cmd.ExecuteNonQuery();
            }
        }
    }

}