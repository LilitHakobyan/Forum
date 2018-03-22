using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Forum.Entity;

namespace Forum.Models
{
    public class PostRepasitory:IDisposable
    {
        private IRepasitory _repo;
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        public PostRepasitory()
        {
            _repo = new Repasitory(connectionString);
        }
        public async Task<IEnumerable<Post>> GetPostsAsync(int threadId)
        {
            List<Post> posts = new List<Post>();
            using (var cmd = _repo.CreateCommand())
            {
                cmd.CommandText = $"SELECT Id,Text,ThreadId,UserId,UserName  FROM Posts Where ThreadId={threadId}";
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        int ID = (int)reader["Id"];
                        posts.Add(new Post()
                        {
                            Id = ID,
                            Text = reader["Text"].ToString(),
                            ThreadId = int.Parse(reader["ThreadId"].ToString()),
                            UserId = Guid.Parse(reader["UserId"].ToString()),
                            UserName = reader["UserName"].ToString()
                        });
                    }
                }
            }
            return posts;
        }
        public async Task<Post> GetById(int id)
        {
            Post post = new Post();
            using (var cmd = _repo.CreateCommand())
            {
                cmd.CommandText = $"SELECT Text,ThreadId,UserId,UserName  FROM Posts Where Id = {id}";
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        post = new Post()
                        {
                            Id = id,
                            Text = reader["Text"].ToString(),
                            ThreadId = int.Parse(reader["ThreadId"].ToString()),
                            UserId = Guid.Parse(reader["UserId"].ToString()),
                            UserName = reader["UserName"].ToString()
                            
                        };
                    }
                }
            }
            return post;
        }
 
        public async Task CreatePostAsync(string Text, int threadId, Guid userId,string UserName)
        {
            using (var cmd = _repo.CreateCommand())
            {
                cmd.CommandText = $"Insert into Posts (Text,ThreadId,UserId,UserName) Values('{Text}',{threadId},'{userId}','{UserName}')";
                await cmd.ExecuteNonQueryAsync();
                _repo.SaveChanges();
            }
        }

        public async Task UpdatePost(Post post)
        {
            using (var cmd = _repo.CreateCommand())
            {
                cmd.CommandText = $"Update  Posts Set Text= '{post.Text}' Where Id={post.Id}";
                await cmd.ExecuteNonQueryAsync();
                _repo.SaveChanges();
            }
        }

        public async Task Remove(int id)
        {
            using (var cmd = _repo.CreateCommand())
            {
                cmd.CommandText = $"Delete From  Posts  Where Id={id}";
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
