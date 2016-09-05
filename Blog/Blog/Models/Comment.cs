using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Blog.Models
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
        [Display(Name = "Author Name:")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Webadress is required")]
        [DataType(DataType.Url)]
        [Display(Name = "Web adress:")]
        public string Web { get; set; }

        [Required(ErrorMessage = "Content is required")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Content:")]
        public string Content { get; set; }

        [Display(Name = "Post:")]
        public Post Post { get; set; }

        [Required(ErrorMessage = "Date is required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Publish Date:")]
        public DateTime Date { get; set; }
    }
}