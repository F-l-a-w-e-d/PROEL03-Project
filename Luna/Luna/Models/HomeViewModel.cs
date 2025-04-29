using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luna.Models
{
    class HomeViewModel
    {
        public List<Post> Posts { get; } = new List<Post>();

        public HomeViewModel()
        {
            Posts.Add(new Post
            {
                UserName = "John Doe",
                TimePosted = DateTime.Now,
                Content = "This is my first post!"
            });
        }
    }
}
