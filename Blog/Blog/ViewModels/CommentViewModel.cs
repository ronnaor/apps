using Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.ViewModels
{
    public class CommentViewModel
    {
        public string PostTitle { get; set; }
        public List<Comment>  Values { get; set; }
    }
}