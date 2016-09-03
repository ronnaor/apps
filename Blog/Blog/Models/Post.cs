﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Blog.Models
{
    public class Post
    {

        public int PostID { get; set; }

        [Required(ErrorMessage = "Post must have title")]
        [DataType(DataType.Text)]
        [Display(Name = "Post Title:")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Author name is required")]
        [RegularExpression(@"([\p{L}'-]+) ([\p{L}'-]+)", ErrorMessage = "Must be only chars")]
        [Display(Name = "Author Name:")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Webadress is required")]
        [DataType(DataType.Url)]
        [Display(Name = "Web adress:")]
        public string Web { get; set; }

        [Required(ErrorMessage = "Date is required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Publish Date:")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Content is required")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Content:")]
        public int Content { get; set; }

        [DataType(DataType.ImageUrl)]
        [Display(Name = "Image:")]
        public int Image { get; set; }

        [DataType(DataType.Url)]
        [Display(Name = "Video:")]
        public int Video { get; set; }

        [Display(Name = "List:")]
        public IList<Blog.Models.Comment> Comments { get; set; }
    }

        public class PostDBContext : DbContext
        {
            public DbSet<Post> Posts { get; set; }
        }
    }