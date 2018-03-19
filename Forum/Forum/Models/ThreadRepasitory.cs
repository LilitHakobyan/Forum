using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Forum.Entity;

namespace Forum.Models
{
    public class ThreadRepasitory : IDisposable
    {
        private Repasitory _repo;
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        public ThreadRepasitory()
        {
            _repo = new Repasitory(connectionString);
        }
        public async Task<IEnumerable<Thread>> GetThreadsAsync(int topicId)
        {
            List<Thread> threads = new List<Thread>();
            using (var cmd = _repo.CreateCommand())
            {
                cmd.CommandText = $"SELECT Id,[Name],TextDescription,TopicId,CreatedAt,UserId  FROM Threads Where TopicId={topicId}";
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        int ID = (int)reader["Id"];
                        threads.Add(new Thread()
                        {
                            Id = ID,
                            Name = reader["Name"].ToString(),
                            CreatedAt = DateTime.Parse(reader["CreatedAt"].ToString()),
                            TextDescription = reader["TextDescription"].ToString(),
                            TopicId = Int32.Parse(reader["TopicId"].ToString()),
                            UserId = Guid.Parse(reader["UserId"].ToString()),
                            //ThreadPosts = await GetThreadPostsAsync(ID)
                        });
                    }
                }
            }
            return threads;
        }
        public async Task<Thread> GetById(int id)
        {
            Thread thread = new Thread();
            using (var cmd = _repo.CreateCommand())
            {
                cmd.CommandText = $"SELECT [Name],TextDescription,TopicId,CreatedAt,UserId FROM Threads WHERE Id = {id}";
                cmd.Parameters.AddWithValue("id", id);
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        //int ID = (int)reader["Id"];
                        thread = new Thread()
                        {
                            Id = id,
                            Name = reader["Name"].ToString(),
                            CreatedAt = DateTime.Parse(reader["CreatedAt"].ToString()),
                            TextDescription = reader["TextDescription"].ToString(),
                            TopicId = Int32.Parse(reader["TopicId"].ToString()),
                            UserId = Guid.Parse(reader["UserId"].ToString()),
                          //  ThreadPosts = await GetThreadPostsAsync(id)
                        };
                    }
                }
            }
            return thread;
        }

        public async Task<List<Post>> GetThreadPostsAsync(int ThreadId)
        {
            List<Post> posts = new List<Post>();
            using (var cmd = _repo.CreateCommand())
            {
                cmd.CommandText =$"Select Id,Text,ThreadId,UserId from Posts WHERE Post.ThreadId={ThreadId}";
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        posts.Add(new Post()
                        {
                            Id = (int)reader["Id"],
                            Text = (string)reader["Text"],
                            ThreadId = (int)reader["ThreadId"],
                            UserId = (Guid)reader["UserId"]
                        });
                    }
                }
            }
            return posts;
        }

        public async Task CreateThreadAsync(string Name, string Text, int TopicId, Guid UserId)
        {
            using (var cmd = _repo.CreateCommand())
            {
                cmd.CommandText = $"Insert into Threads (Name,TextDescription,TopicId,UserId,CreatedAt) Values('{Name}','{Text}',{TopicId},'{UserId}','{DateTime.Now:s}')";
                await cmd.ExecuteNonQueryAsync();
                _repo.SaveChanges();
            }
        }

        public async Task UpdateThread(Thread thread)
        {
            using (var cmd = _repo.CreateCommand())
            {
                cmd.CommandText = $"Update  Threads Set Name= '{thread.Name}',TextDescription='{thread.TextDescription}' Where Id={thread.Id}";
                await cmd.ExecuteNonQueryAsync();
                _repo.SaveChanges();
            }
        }

        public async Task Remove(int id)
        {
            using (var cmd = _repo.CreateCommand())
            {
                cmd.CommandText = $"Delete From  Threads  Where Id={id}";
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