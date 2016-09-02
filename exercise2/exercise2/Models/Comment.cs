using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace exercise2.Models
{
    public class Comment
    {

        public int CommentID { get; set; }

        public int PostID { get; set; }

        [Required(ErrorMessage = "Comment must have title")]
        [DataType(DataType.Text)]
        [Display(Name = "Comment Title:")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Author name is required")]
        [RegularExpression(@"([\p{L}'-]+) ([\p{L}'-]+)", ErrorMessage = "Must be only chars")]
        [Display(Name = "Author Name:")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Webadress is required")]
        [DataType(DataType.Url)]
        [Display(Name = "Web adress:")]
        public string Web { get; set; }

        [Required(ErrorMessage = "Content is required")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Content:")]
        public int Content { get; set; }

        [Display(Name = "Post:")]
        public Post Post { get; set; }

    }

    public class CommentDBContext : DbContext
    {
        public DbSet<Comment> Comments { get; set; }
    }
}
