using System;
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
                        int ID = (int) reader["Id"];
                        topics.Add(new Topic()
                        {
                            Id= ID,
                            Name = (string)reader["Name"],
                        });
                    }
               
                }
                   _repo.SaveChanges();
                foreach (var topic in topics)
                {
                    topic.TopicThreads = await GetTopicThreads(topic.Id);
                }
            }
            return topics;
        }
        public async Task<Topic> GetById(int id)
        {
            string name=string.Empty;
            using (var cmd = _repo.CreateCommand())
            {
                cmd.CommandText = $"SELECT Name FROM Topics WHERE Id = {id}";
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        name = (string) reader["Name"];
                    }
                    _repo.SaveChanges();
                }
            }
            return new Topic {Id = id, Name = name,TopicThreads =  await GetTopicThreads(id) };
        }

        public async Task<List<Thread>> GetTopicThreads(int TopicId)
        {
              List<Thread> threads=new List<Thread>();
            using (var cmd = _repo.CreateCommand())
            {
                cmd.CommandText = $"Select Id,Name,TextDescription from Threads WHERE Threads.TopicId={TopicId}";
                using (var reader =  await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        threads.Add(new Thread
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

        public async Task CreateTopicAsync(string Name)
        {
            using (var cmd =  _repo.CreateCommand())
            {
                cmd.CommandText = $"Insert into Topics (Name) Values('{Name}')";
                await  cmd.ExecuteNonQueryAsync();
                _repo.SaveChanges();
            }
        }

        public async Task UpdateTopic(Topic topic)
        {
            using (var cmd = _repo.CreateCommand())
            {
                cmd.CommandText = $"Update  Topics Set Name= '{topic.Name}' Where Id={topic.Id}";
                await cmd.ExecuteNonQueryAsync();
                _repo.SaveChanges();
            }
        }

        public async Task Remove(int id)
        {
            using (var cmd = _repo.CreateCommand())
            {
                cmd.CommandText = $"Delete From  Topics  Where Id={id}";
                await cmd.ExecuteNonQueryAsync();
                _repo.SaveChanges();
            }
        }
        public void Dispose()
        {
            _repo.Dispose();
        }
    }

}