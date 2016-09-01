using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace exercise2.Models
{
    public class Post
    {

        public int ID { get; set; }

        [Required(ErrorMessage = "Post must have title")]
        [DataType(DataType.Text)]
        [Display(Name = "Post Title:")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Author name is required")]
        [RegularExpression(@"([\p{L}'-]+) ([\p{L}'-]+)", ErrorMessage = "Must be only chars")]
        [Display(Name = "Name:")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Webadress is required")]
        [DataType(DataType.Website)]
        [Display(Name = "Webadress:")]
        public string Web { get; set; }

        [Required(ErrorMessage = "Date is required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date:")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Content is required")]
        [DataType(DataType.Text)]
        [Display(Name = "Content:")]
        public int Content { get; set; }

        [DataType(DataType.Image)]
        [Display(Name = "Image:")]
        public int Image { get; set; }

        [DataType(DataType.Video)]
        [Display(Name = "Video:")]
        public int Video { get; set; }

        public void copy(Fan fan)
        {
            Name = fan.Name;
            LastName = fan.LastName;
            Gender = fan.Gender;
            BirthDate = fan.BirthDate;
            Seniority = fan.Seniority;
        }

    }

    public class PostDBContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }
    }
}
