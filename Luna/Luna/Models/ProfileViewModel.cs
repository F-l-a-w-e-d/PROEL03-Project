using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Luna.Models
{
    class ProfileViewModel : INotifyPropertyChanged
    {
        // INotifyChanged adding
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string name = null)
          => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        // acc
        Account _account;
        public Account account
        {
            get => _account;
            set
            {
                _account = value;
                OnPropertyChanged();
            }
        }

        // posts
        List<Post> _posts;
        public List<Post> posts
        {
            get => _posts;
            set { _posts = value; OnPropertyChanged(); }
        }

        string _postCount;
        public string PostCount 
        {
            get => _postCount;
            set { _postCount = value; OnPropertyChanged(); } 
        }

        // commands
        public ICommand deleteCommand { get; set; }
        public ICommand updateCommand { get; set; }
        public ICommand gotoHome { get; set; }
        public ICommand refresh { get; set; }

        // Initialize
        public ProfileViewModel() 
        {
            updateCommand = new Command<Post>(async (post) => await updatePost(post));
            deleteCommand = new Command<Post>(async (post) => await deletePost(post));
            gotoHome = new Command(() => Application.Current.MainPage = App.Services.GetRequiredService<Home>());
            refresh = new Command(() => loadPosts());

            loadAccount();
        }

        // go to an Edit UI
        async Task updatePost(Post post)
        {
            var editPage = App.Services.GetRequiredService<EditPage>();
            editPage.BindingContext = new EditPostViewModel(post);

            Application.Current.MainPage = editPage;
        }

        // delete a post
        async Task deletePost(Post post)
        {
            if (!await MockAPI.hasAnInternetConnection()) { return; }

            if (await Application.Current.MainPage.DisplayAlert("Delete Post", "Are your sure you want to delete the post?", "Yes", "No"))
            {
                post.isDeleted = true;

                if (!await MockAPI.updateData(post, "Post", post.PostId))
                {
                    Application.Current.MainPage.DisplayAlert("Notice", "Delete post failed, please try again.", "OK.");
                }
                else
                {
                    loadPosts();
                    Application.Current.MainPage.DisplayAlert("Notice", "Delete post successful", "OK.");
                }
            }
        }

        // load the current user
        async Task loadAccount()
        {
            List<Account> accounts = await MockAPI.fetchAllData<Account>("Account");
            account = accounts.FirstOrDefault(a => a.AccountId == (string)Preferences.Get("currentUser", "0"));

            loadPosts();
        }

        // load current user's posts
        public async Task loadPosts()
        {
            List<Post> posts = await MockAPI.fetchAllData<Post>("Post");
            posts = posts.Where(p => p.AccountId == account.AccountId && !p.isDeleted).OrderByDescending(p => p.TimePosted).ToList();
            posts.ForEach(p => p.Account = account);
            this.posts = posts;

            PostCount = posts.Count.ToString();
        }
    }
}
