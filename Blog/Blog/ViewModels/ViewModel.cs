using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.ViewModels
{
    public class ViewModel
    {
        public IEnumerable<FanManager> FanManager { get; set; }
        public IEnumerable<CommentPost> CommPost { get; set; }
    }
}