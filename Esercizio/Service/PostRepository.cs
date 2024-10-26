// Service/PostRepository.cs
using Esercizio.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace Esercizio.Service
{
    public class PostRepository
    {
        private readonly string _connectionString;

        public PostRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public void AddPost(Post post)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Posts (UserId, Content, CreatedAt) VALUES (@UserId, @Content, @CreatedAt)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserId", post.UserId);
                command.Parameters.AddWithValue("@Content", post.Content);
                command.Parameters.AddWithValue("@CreatedAt", DateTime.Now);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public List<Post> GetPostsByUserId(int userId)
        {
            List<Post> posts = new List<Post>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Posts WHERE UserId = @UserId ORDER BY CreatedAt DESC";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserId", userId);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    posts.Add(new Post
                    {
                        Id = (int)reader["Id"],
                        UserId = (int)reader["UserId"],
                        Content = reader["Content"].ToString(),
                        CreatedAt = (DateTime)reader["CreatedAt"]
                    });
                }
            }
            return posts;
        }

        public void DeletePost(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM Posts WHERE Id = @Id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
