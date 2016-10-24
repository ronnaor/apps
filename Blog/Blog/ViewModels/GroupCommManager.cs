using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Blog.ViewModels
{
    public class GroupCommManager
    {
        [Display(Name = "Post Title")]
        public string PostTitle { get; set; }

        public List<CommentManager> Values { get; set; }
    }
}