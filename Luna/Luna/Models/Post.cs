using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luna.Models
{
    class Post
    {
        // For the newsfeed
        public string UserName { get; set; }
        public DateTime TimePosted { get; set; }
        public string Content { get; set; }
    }
}
