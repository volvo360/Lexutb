using System;

namespace Bloggy.Demo.Domain
{
    public class BlogPost
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }

        //Date time stamp for the post on the blog

        public DateTime Ts { get; set; }
    }
}
