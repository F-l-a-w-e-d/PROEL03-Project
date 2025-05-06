using CommunityToolkit.Maui.Converters;
using Microsoft.Maui.ApplicationModel.Communication;
using Microsoft.Maui.Controls;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
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
        public ICommand backToLogin { get; set; }
        public ICommand goToRegister { get; set; }

        // view model
        public LoginViewModel()
        {
            register = new Command<Account>(async (acc) => await RegisterAccount(acc));
            login = new Command<Account>(async (acc) => await loginAcc(acc));

            backToLogin = new Command(() => Application.Current.MainPage = App.Services.GetRequiredService<MainPage>());
            goToRegister = new Command(() => Application.Current.MainPage = App.Services.GetRequiredService<Registration>());
        }

        private async Task RegisterAccount(Account acc)
        {
            // a function for checking numbers on names
            bool hasNumericName(string name)
            {
                for (int i = 0; i < name.Length; i++)
                {
                    if (int.TryParse(name[i].ToString(), out _)) return true;
                }
                return false;
            }

            #region ----- VALIDATIONS -----
            // check if null
            if (acc == null || string.IsNullOrWhiteSpace(acc.user) || string.IsNullOrWhiteSpace(acc.pass) ||
                string.IsNullOrWhiteSpace(acc.Fname) || string.IsNullOrWhiteSpace(acc.Lname) || string.IsNullOrWhiteSpace(acc.Mname) || string.IsNullOrWhiteSpace(acc.address) || string.IsNullOrWhiteSpace(acc.BirthDate.ToString()) || string.IsNullOrWhiteSpace(acc.contactNo.ToString()))
            {
                await Application.Current.MainPage.DisplayAlert("Register", "Please do not provide blanks. Try again.", "OK");
                return;
            }

            // NAMES - Consists numeric
            if (hasNumericName(acc.Fname) || hasNumericName(acc.Mname) || hasNumericName(acc.Lname))
            {
                await Application.Current.MainPage.DisplayAlert("Register", "Please enter a valid name.", "OK");
                return;
            }

            // BIRTH DATE - Age
            int age = DateTime.Now.Year - acc.BirthDate.Year;
            if (age < 13 || age > 120)
            {
                await Application.Current.MainPage.DisplayAlert("Register", "No age should be less than 13 or greater than 120", "OK");
                return;
            }

            // CONTACTNO - Consists text
            if (!long.TryParse(acc.contactNo, out _) || acc.contactNo.Length < 11)
            {
                await Application.Current.MainPage.DisplayAlert("Register", "Please enter a valid contact number.", "OK");
                return;
            }

            // USER (Email) - validation
            if (!Regex.IsMatch(acc.user, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                await Application.Current.MainPage.DisplayAlert("Register", "Please enter a valid username.", "OK");
                return;
            }

            // Password - length
            if (acc.pass.Length < 8)
            {
                await Application.Current.MainPage.DisplayAlert("Register", "Password must contain 8 letters!", "OK");
                return;
            }

            // PASSWORD - Matching confirm pass
            if (acc.pass != acc.confirmPass)
            {
                await Application.Current.MainPage.DisplayAlert("Register", "Password do not match!", "OK");
                return;
            }

            // check if user exists
            List<Account> accounts = await MockAPI.fetchAllData<Account>("Account");
            if (accounts.Any(a => a.user == acc.user))
            {
                await Application.Current.MainPage.DisplayAlert("Register", "Account already exists!", "OK");
                return;
            }
            #endregion

            // inserting of data
            if (await MockAPI.insertData(acc, "Account"))
            {
                await Application.Current.MainPage.DisplayAlert("Register", "Successfully registered!", "OK");
                Application.Current.MainPage = App.Services.GetRequiredService<MainPage>();
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Register", "Register Failed. Please try again.", "OK");
            }
        }

        // login command
        private async Task loginAcc(Account acc)
        {
            // check if empty
            string user = acc.user ?? "";
            string pass = acc.pass ?? "";

            // get account
            List<Account> accounts = await MockAPI.fetchAllData<Account>("Account");
            Account account = accounts.FirstOrDefault(a => a.user == user && a.pass == pass);

            // restriction if account is null
            if (account == null)
            {
                await Application.Current.MainPage.DisplayAlert("Login", "Incorrect username or passsord. Please try again.", "OK");
                return;
            }

            // success
            await Application.Current.MainPage.DisplayAlert("Login", "Successfully logged in! Welcome " + account.Lname, "OK");

            Application.Current.MainPage = App.Services.GetRequiredService<Home>();
        }
    }
}
