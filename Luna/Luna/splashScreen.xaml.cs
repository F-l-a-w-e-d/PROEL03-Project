using System;
using System.Diagnostics;
using Microsoft.Maui.Controls;

namespace Luna
{
    public partial class splashScreen : ContentPage
    {
        public splashScreen()
        {
            InitializeComponent();
        }

        // once the video has ended, proceed to login
        private async void OnMediaEnded(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }
    }
}

