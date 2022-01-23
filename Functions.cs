using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfBloggyImproved
{
    public class Functions
    {
        static BlogContext context = new BlogContext();

        //The function remains only for if you want to develop this program in the future and may need to restart from the beginning in a simple way.

        public static void ClearDatabase()
        {
            context.RemoveRange(context.BlogPosts);

            context.SaveChanges();
        }

        public static void AddSomeDefaultData()
        {
            context.BlogAuthors.Add(new BlogAuthor () { Author = "Lily", DateAdded = DateTime.Now.AddDays(-1) });
            context.BlogAuthors.Add(new BlogAuthor() { Author = "Ethan", DateAdded = DateTime.Now.AddDays(-1).AddHours(1)});
            context.SaveChanges();

            BlogAuthor author = context.BlogAuthors.Single(c => c.Id == 1);

            context.BlogPosts.Add(new BlogPost() { Title = "The sun is bright", PostTime = DateTime.Now, blogAuthor = author });

            author = context.BlogAuthors.Single(c => c.Id == 2);
            context.BlogPosts.Add(new BlogPost() { Title = "I will go swimming", PostTime = DateTime.Now.AddMinutes(-10), blogAuthor = author });
            context.SaveChanges();
        }

        public static void AddPost(string title = null, int id = 0)
        {
            if (title == null || id == 0)
            {
                return;
            }

            BlogAuthor author = context.BlogAuthors.Single(c => c.Id == id);

            context.BlogPosts.Add(new BlogPost() { Title = title, blogAuthor = author });
            context.SaveChanges();
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

        public static void DeleteBloger(int id = 0)
        {
            if (id <= 0)
            {
                return;
            }

            var itemToRemove = context.BlogAuthors.SingleOrDefault(x => x.Id == id); //returns a single item.

            if (itemToRemove != null)
            {
                context.BlogAuthors.Remove(itemToRemove);
                context.SaveChanges();
            }
        }

        public static void AddBloger(BlogAuthor author)
        {
            if (author == null)
            {
                return;
            }

            context.BlogAuthors.Add(author);
            context.SaveChanges();
        }
    }
}
