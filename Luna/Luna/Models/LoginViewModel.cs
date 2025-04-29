using Microsoft.Maui.Controls;
using System.Windows.Input;

namespace Luna.Models
{
    public class LoginViewModel
    {
        // bind for XAML
        public Account acc { get; set; } = new Account();

        // used for storing lists
        public static List<Account> accounts = new List<Account>();

        // commands
        public ICommand register { get; set; }
        public ICommand login { get; set; }

        // view model
        public LoginViewModel()
        {
            register = new Command<Account>(async (acc) => await RegisterAccount(acc));
            login = new Command<Account>(async (acc) => await loginAcc(acc));
        }

        private async Task RegisterAccount(Account acc)
        {
            // check if null
            if (acc == null || string.IsNullOrWhiteSpace(acc.user) || string.IsNullOrWhiteSpace(acc.pass) ||
                string.IsNullOrWhiteSpace(acc.Fname) || string.IsNullOrWhiteSpace(acc.Lname) || string.IsNullOrWhiteSpace(acc.Mname) || string.IsNullOrWhiteSpace(acc.address) || string.IsNullOrWhiteSpace(acc.Age.ToString()) || string.IsNullOrWhiteSpace(acc.contactNo.ToString()))
            {
                await Application.Current.MainPage.DisplayAlert("Register", "Please do not provide blanks. Try again.", "OK");
                return;
            }

            // check if password match
            if (acc.pass != acc.confirmPass)
            {
                await Application.Current.MainPage.DisplayAlert("Register", "Password do not match!", "OK");
                return;
            }

            // check if user exists
            if (accounts.Any(a => a.user == acc.user))
            {
                await Application.Current.MainPage.DisplayAlert("Register", "Account already exists!", "OK");
                return;
            }

            // success
            accounts.Add(acc);

            await Application.Current.MainPage.DisplayAlert("Register", "Successfully registered!", "OK");
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        // login command
        private async Task loginAcc(Account acc)
        {
            // check if empty
            string user = acc.user ?? "";
            string pass = acc.pass ?? "";

            // get account
            Account account = accounts.FirstOrDefault(a => a.user == user && a.pass == pass);

            // restriction if account is null
            if (account == null)
            {
                await Application.Current.MainPage.DisplayAlert("Login", "Incorrect username or passsord. Please try again.", "OK");
                return;
            }

            // success
            await Application.Current.MainPage.DisplayAlert("Login", "Successfully logged in! Welcome " + account.Lname, "OK");
        }
    }
}
