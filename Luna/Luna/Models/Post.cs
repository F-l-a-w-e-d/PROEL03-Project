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
        public string PostId { get; set; }
        public string AccountId { get; set; }
        public Account Account { get; set; }
        public DateTime TimePosted { get; set; }
        public string Content { get; set; }
        public bool isDeleted { get; set; }
    }
}
