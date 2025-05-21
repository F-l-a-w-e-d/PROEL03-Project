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
    class HomeViewModel : INotifyPropertyChanged
    {
        // INotifyChanged adding
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string name = null)
          => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        // Get Posts
        List<Post> _posts;
        public List<Post> Posts
        {
            get => _posts;
            set { _posts = value; OnPropertyChanged(); }
        }

        // Commands
        public ICommand createPost { get; set; } 
        public ICommand gotoProfile { get; set; } 
        public ICommand Logout { get; set; } 
        public ICommand refresh { get; set; } 

        // Initialize data
        public HomeViewModel()
        {
            createPost = new Command(() => Application.Current.MainPage = App.Services.GetRequiredService<NewPost>());
            gotoProfile = new Command(() => Application.Current.MainPage = App.Services.GetRequiredService<Profile>());
            refresh = new Command(() => loadPosts());

            Logout = new Command(() =>
            {
                Preferences.Clear();
                Application.Current.MainPage = App.Services.GetRequiredService<MainPage>();
            });

            loadPosts();
        }

        // load the posts
        public async Task loadPosts()
        {
            List<Account> accounts = await MockAPI.fetchAllData<Account>("Account");
            List<Post> posts = await MockAPI.fetchAllData<Post>("Post");
            posts = posts.Where(p => !p.isDeleted).OrderByDescending(p => p.TimePosted).ToList();
            posts.ForEach(p => p.Account = accounts.FirstOrDefault(a => a.AccountId == p.AccountId));
            Posts = posts;
        }
    }
}
