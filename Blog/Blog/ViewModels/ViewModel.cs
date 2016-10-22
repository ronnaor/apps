using Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.ViewModels
{
    public class ViewModel
    {
        public IEnumerable<FanManager> FanManager { get; set; }
        public IEnumerable<CommentManager> CommManager { get; set; }
        public IEnumerable<CommentViewModel> CommentsGtoup { get; set; }
    }
}