using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Luna.Models
{
    class EditPostViewModel
    {
        // put variable
        public Post Post { get; }
        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public EditPostViewModel(Post post)
        {
            Post = post;

            // save the changes of the post
            SaveCommand = new Command(async () => {
                if (string.IsNullOrEmpty(Post.Content))
                {
                    Application.Current.MainPage.DisplayAlert("Notice", "Please provide a content", "OK.");
                }

                // call MockAPI.PUT or PATCH here
                if(!await MockAPI.updateData(Post, "Post", Post.PostId))
                {
                    Application.Current.MainPage.DisplayAlert("Notice", "Updating post failed, please try again.", "OK.");
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Notice", "Updating post successful.", "OK.");
                    Application.Current.MainPage = App.Services.GetRequiredService<Profile>();
                }
            });

            // cancel the edit.
            CancelCommand = new Command(() => Application.Current.MainPage = App.Services.GetRequiredService<Profile>());
        }
    }
}
