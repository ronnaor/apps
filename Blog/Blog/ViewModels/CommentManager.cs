using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Blog.ViewModels
{
    public class CommentManager
    {

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public string UserName { get; set; }

        [Display(Name = "Post Title")]
        public string PostTitle { get; set; }

        [Display(Name = "Comment Title")]
        public string CommentTitle { get; set; }

        [Display(Name = " Comment Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CommentDate { get; set; }
    }
}