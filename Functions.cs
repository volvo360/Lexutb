using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfBloggy
{
    public class Functions
    {
        static BlogContext context = new BlogContext();

        public static void ClearDatabase()
        {
            context.RemoveRange(context.BlogPosts);


            context.SaveChanges();
        }

        public static void AddSomeTitles()
        {

        var lilyPost = new BlogPost { Title = "The sun is bright", Author = "Lily" };
        var ethanPost = new BlogPost { Title = "I will go swimming", Author = "Ethan" };

            using (var context = new BlogContext())
            {
                context.BlogPosts.AddRange(lilyPost, ethanPost);
                context.SaveChanges();
            }
        }

        public static void AddPost(string title = null, string writer = null)
        {
            if (title == null || writer == null)
            {
                return;
            }

            using (var context = new BlogContext())
            {
                var newPost = new BlogPost { Title = title, Author = writer };

                context.BlogPosts.Add(newPost);
                context.SaveChanges();
            }
        }

        public static void RemovePost(int id = 0)
        {
            var itemToRemove = context.BlogPosts.SingleOrDefault(x => x.Id == id); //returns a single item.

            if (itemToRemove != null) {
                context.BlogPosts.Remove(itemToRemove);
                context.SaveChanges();
            }
            else
            {
                return ;
            }
        }
    }
}
