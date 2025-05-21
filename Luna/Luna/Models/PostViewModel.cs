using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Luna.Models
{
    class PostViewModel
    {
        // binding for post xaml
        public Post post { get; set; } = new Post();
        public ICommand PostCommand { get; set; }
        public ICommand GobackCommand { get; set; }

        public PostViewModel()
        {
            // add functions
            PostCommand = new Command<Post>(async (post) => await postCommand(post));

            GobackCommand = new Command(() => Application.Current.MainPage = App.Services.GetRequiredService<Home>());
        }

        async Task postCommand(Post post)
        {
            // check if content is not empty
            if (post.Content == null || post.Content.Trim() == "")
            {
                Application.Current.MainPage.DisplayAlert("Empty Post", "Please provide a post", "OK.");
                return;
            }

            // check if user has internet connection
            if (await MockAPI.hasAnInternetConnection() == false) { return; }

            // fill in properties after validation
            post.TimePosted = DateTime.Now;
            post.AccountId = (string)Preferences.Get("currentUser", "0");
            post.isDeleted = false;

            // insert post
            if (!await MockAPI.insertData(post, "Post"))
            {
                Application.Current.MainPage.DisplayAlert("Notice", "Posting failed, please try again.", "OK.");
                return;
            }

            // go to main page
            Application.Current.MainPage = App.Services.GetRequiredService<Home>();
        }
    }
}
