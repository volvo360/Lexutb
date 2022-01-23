using System.Collections.Generic;
using System.Data.SqlClient;
using Bloggy.Demo.Domain;
using Bloggy.Demo;
using System;

namespace Bloggy.Demo
{
    public class DataAccess
    {
        private string conString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;";

        //Added by Anders Wallin, Helsingborg, 2022-01-18
        public void checkDatabase()
        {
            var masterSQL = $"IF NOT EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = 'BlogPostDemo') " +
                                $"BEGIN " +
                                    $"CREATE DATABASE BlogPostDemo " +
                                $"END \n";

            using (SqlConnection connection = new SqlConnection(conString))
            using (SqlCommand command = new SqlCommand(masterSQL, connection))
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
            }

            masterSQL = $"USE BlogPostDemo " +
                                $"IF NOT EXISTS " +
                                    $" (SELECT[name] FROM sys.tables WHERE[name] = 'BlogPost') " +
                                        $"BEGIN\n" +
                                            $"CREATE TABLE BlogPost(Id int IDENTITY(1,1) PRIMARY KEY, Author INT NOT NULL , ts DATETIME NOT NULL DEFAULT (GETDATE()) , Title TEXT NOT NULL)\n" +

                                            $"INSERT INTO BlogPost (Author, Title, ts) VALUES ('1', 'The sun is bright', SYSDATETIME())\n" +
                                            $"INSERT INTO BlogPost (Author, Title, ts) VALUES ('2', 'I will go swimming', DATEADD(minute,-10, SYSDATETIME()))\n" +
                                        $"END\n" +
                                $"IF NOT EXISTS\n" +
                                    $" (SELECT[name] FROM sys.tables WHERE[name] = 'BlogAuthor') " +
                                        $"BEGIN \n" +
                                            $"CREATE TABLE BlogAuthor (Id int IDENTITY(1,1) PRIMARY KEY, Author VARCHAR(MAX) NOT NULL )\n " +

                                            $"INSERT INTO BlogAuthor (Author) VALUES ('Lily')\n" +
                                            $"INSERT INTO BlogAuthor (Author) VALUES ('Ethan')\n" +
                                        $"END".ToString();
            using (SqlConnection connection = new SqlConnection(conString))
            using (SqlCommand command = new SqlCommand(masterSQL, connection))
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
            }

            conString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BlogPostDemo;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;";

        }

        public List<BlogPost> GetAllBlogPostsBrief()
        {
            var sql = @"SELECT BlogPost.Id, BlogAuthor.Author, [Title], ts FROM BlogPost INNER JOIN BlogAuthor ON BlogPost.Author = BlogAuthor.Id";
            using (SqlConnection connection = new SqlConnection(conString))            
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                var list = new List<BlogPost>();

                while (reader.Read())
                {
                    var bp = new BlogPost
                    {
                        Id = reader.GetSqlInt32(0).Value,
                        Author = reader.GetSqlString(1).Value,
                        Title = reader.GetSqlString(2).Value,
                        Ts = reader.GetSqlDateTime(3).Value
                    };
                    list.Add(bp);
                }

                return list;

            }
        }

        public BlogPost GetPostById(int postId)
        {
            var sql = @"SELECT BlogPost.Id, BlogAuthor.Author, [Title], ts FROM BlogPost 
                INNER JOIN BlogAuthor ON BlogPost.Author = BlogAuthor.Id WHERE BlogPost.Id=@Id";

            using (SqlConnection connection = new SqlConnection(conString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                connection.Open();
                command.Parameters.Add(new SqlParameter("Id", postId));

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    var bp = new BlogPost
                    {
                        Id = reader.GetSqlInt32(0).Value,
                        Author = reader.GetSqlString(1).Value,
                        Title = reader.GetSqlString(2).Value
                    };
                    return bp;

                }

                return null;

            }
        }

        public void UpdateBlogpost(BlogPost blogPost)
        {
            var sql = "UPDATE BlogPost SET Title=@Title WHERE id=@Id";

            using (SqlConnection connection = new SqlConnection(conString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                connection.Open();
                command.Parameters.Add(new SqlParameter("Id", blogPost.Id));
                command.Parameters.Add(new SqlParameter("Title", blogPost.Title));
                command.ExecuteNonQuery();
            }
        }

        //Added by Anders Wallin, Helsingborg, 2022-01-18
        public (bool exists, int authorId) ValidateAuthor(string Author = null)
        {
            if (Author == null || Author == "")
            {
                return (false, 0);
            }

            int AuthorId = 0;

            if (int.TryParse(Author, out AuthorId))
            {
                var sql = "SELECT Id FROM BlogAuthor WHERE id=@id";

                using (SqlConnection connection = new SqlConnection(conString))
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();

                    command.Parameters.Add(new SqlParameter("id", AuthorId.ToString()));

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        return (true, AuthorId);

                    }
                    else
                    {
                        return (false, AuthorId);
                    }
                }
            }

            else
            {
                var sql = "SELECT * FROM BlogAuthor WHERE LOWER(BlogAuthor.Author)=@AuthorSearch";

                using (SqlConnection connection = new SqlConnection(conString))
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();

                    command.Parameters.Add(new SqlParameter("AuthorSearch", Author.ToLower()));

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        AuthorId = reader.GetInt32(0);
                        return (true, AuthorId);

                    }
                    else
                    {
                        return (false, AuthorId);
                    }
                }
            }
        }

        //Added by Anders Wallin, Helsingborg, 2022-01-18
        public List<BlogAuthor> ListAuthors()
        {
            var sql = "SELECT * FROM BlogAuthor ORDER BY LOWER(BlogAuthor.Author)";

            using (SqlConnection connection = new SqlConnection(conString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                var AuthorList = new List<BlogAuthor>();

                while (reader.Read())
                {
                    var temp = new BlogAuthor();
                    {
                        temp.Id = reader.GetSqlInt32(0).Value;
                        temp.AuthorName = reader.GetSqlString(1).Value;                        
                    };
                    AuthorList.Add(temp);
                }

                return AuthorList;
            }
        }

        //Added by Anders Wallin, Helsingborg, 2022-01-18
        public void AddBlogPost(BlogPost post)
        {
            try
            {
                var sql = "INSERT INTO  BlogPost (Author, Title) VALUES (@Author, @Ttile)";

                using (SqlConnection connection = new SqlConnection(conString))
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();

                    command.Parameters.Add(new SqlParameter("Author", post.Author));
                    command.Parameters.Add(new SqlParameter("Ttile", post.Title));

                    SqlDataReader reader = command.ExecuteReader();

                    var AuthorList = new List<BlogAuthor>();

                    while (reader.Read())
                    {
                        var temp = new BlogAuthor();
                        {
                            temp.Id = reader.GetSqlInt32(0).Value;
                            temp.AuthorName = reader.GetSqlString(1).Value;
                        };
                        AuthorList.Add(temp);
                    }
                }
            }
            catch
            {
                Console.Write("Vi kunde inte addera posten till databasen!!!\n");
            }
        }

        //Added by Anders Wallin, Helsingborg, 2022-01-18
        public void deleteBlogPost(int postId = 0)
        {
            if (postId == 0)
            {
                return; 
            }

            var sql = "DELETE FROM BlogPost  WHERE id=@Id";

            using (SqlConnection connection = new SqlConnection(conString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                connection.Open();
                command.Parameters.Add(new SqlParameter("Id", postId));
                command.ExecuteNonQuery();
            }
        }

        //Added by Anders Wallin, Helsingborg, 2022-01-18
        public List<BlogAuthor> GetAllWriters()
        {
            var sql = "SELECT * FROM BlogAuthor ORDER BY LOWER(BlogAuthor.Author)";

            using (SqlConnection connection = new SqlConnection(conString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                List<BlogAuthor> list = new List<BlogAuthor>();

                while (reader.Read())
                {
                    var author = new BlogAuthor();

                    author.Id = reader.GetInt32(0);
                    author.AuthorName = reader.GetString(1);

                    list.Add(author);
                }

                return list;
            }
        }

        //Added by Anders Wallin, Helsingborg, 2022-01-18
        public (bool exists, BlogAuthor data) CheckIfWriterExists(object author)
        {
            string sql = null;

            string temp = author.GetType().Name;

            if (author.GetType().Name == "String")
            {
                sql = "SELECT * FROM BlogAuthor WHERE LOWER(BlogAuthor.Author) = @author";
            }
            else if (author.GetType().Name == "Int32")
            {
                sql = "SELECT * FROM BlogAuthor WHERE id = @author";
            }


            using (SqlConnection connection = new SqlConnection(conString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                connection.Open();

                command.Parameters.Add(new SqlParameter("author", author));

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    BlogAuthor authorPost = new BlogAuthor();

                    authorPost.Id = reader.GetInt32(0);
                    authorPost.AuthorName = reader.GetString(1);

                    return (true, authorPost);
                }
                else
                {
                    return (false, null);
                }
            }
        }

        //Added by Anders Wallin, Helsingborg, 2022-01-18
        public bool AddBlogWriter(string author)
        {
            try
            {
                var sql = "INSERT INTO  BlogAuthor (Author) VALUES (@Author)";

                using (SqlConnection connection = new SqlConnection(conString))
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();

                    command.Parameters.Add(new SqlParameter("Author", author));
                    
                    command.ExecuteNonQuery();

                    return true;
                }
            }
            catch
            {
                Console.Write("Vi kunde inte addera posten till databasen!!!\n");

                return false;
            }
        }

        //Added by Anders Wallin, Helsingborg, 2022-01-18
        public void UpdateBlogWritert(BlogAuthor author)
        {
            var sql = "UPDATE BlogAuthor SET Author=@AuthorName WHERE id=@Id";

            using (SqlConnection connection = new SqlConnection(conString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                connection.Open();
                command.Parameters.Add(new SqlParameter("Id", author.Id));
                command.Parameters.Add(new SqlParameter("AuthorName", author.AuthorName));
                command.ExecuteNonQuery();
            }
        }

        //Added by Anders Wallin, Helsingborg, 2022-01-18
        public void DeleteBlogWriter(int id)
        {
            //Delete user from author table

            var sql = "DELETE FROM BlogAuthor WHERE id=@Id";

            using (SqlConnection connection = new SqlConnection(conString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                connection.Open();
                command.Parameters.Add(new SqlParameter("Id", id));
                command.ExecuteNonQuery();
            }

            //Delete blog posts connected to that user

            sql = "DELETE FROM BlogPost WHERE id=@Id";

            using (SqlConnection connection = new SqlConnection(conString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                connection.Open();
                command.Parameters.Add(new SqlParameter("Id", id));
                command.ExecuteNonQuery();
            }
        }
    }
}
