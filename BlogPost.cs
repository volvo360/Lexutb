using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EfBloggyImproved
{ 
    public class BlogAuthor
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Author { get; set; }
        public int? BlogPostId { get; set; }

        public DateTime DateAdded { get; set; }
        public List<BlogPost>? blogPost { get; set; }

    }

    public class BlogPost
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime PostTime { get; set; }
        public int? AuthorId { get; set; }
        public BlogAuthor blogAuthor { get; set; }
    }
}
