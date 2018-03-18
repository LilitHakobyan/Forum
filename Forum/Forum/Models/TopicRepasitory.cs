﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Forum.Entity;
using System.Configuration;
using System.Threading.Tasks;

namespace Forum.Models
{
    public class TopicRepasitory:IDisposable
    {
        private Repasitory _repo;
        private readonly string connectionString=ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        public TopicRepasitory()
        {
            _repo = new Repasitory(connectionString);
        }
        public async Task<IEnumerable<Topic>> GetTopics()
        {
            List<Topic> topics=new List<Topic>();
            using (var cmd = _repo.CreateCommand())
            {
                cmd.CommandText = "SELECT Id,Name FROM Topics";
                using (var reader =await cmd.ExecuteReaderAsync())
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
        public async Task<Topic> GetById(int id)
        {
            string name=string.Empty;
            using (var cmd = _repo.CreateCommand())
            {
                cmd.CommandText = "SELECT Name FROM Topics WHERE Id = @id";
                cmd.Parameters.AddWithValue("id", id);
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        name = (string) reader["Name"];
                    }
                }
            }
            return new Topic {Id = id, Name = name,TopicThreads =GetTopicThreads(id) };
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
                cmd.CommandText = $"Insert into Topics (Name) Values('{Name}')";
                cmd.ExecuteNonQuery();
                _repo.SaveChanges();
            }
        }

        public void Dispose()
        {
            _repo.Dispose();
        }
    }

}