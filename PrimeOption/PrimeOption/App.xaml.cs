using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PrimeOption.Services;
using PrimeOption.Views;

namespace PrimeOption
{
    public partial class App : Application
    {

        public App ()
        {
            InitializeComponent();
            var isLoggedIn = CheckIfUserIsLoggedIn();

            //if (isLoggedIn)
            //{
            //    // If the user is already logged in, navigate to the main application page (e.g., AboutPage or HomePage)
            //    MainPage = new AppShell(); // Or whatever your main page is (Shell-based navigation or a simple page)
            //}
            //else
            //{
            //    // If the user is not logged in, show the LoginPage
            //    MainPage = new NavigationPage(new LoginPage());
            //}
            MainPage = new NavigationPage(new LoginPage());
        }

        private bool CheckIfUserIsLoggedIn()
        {
            // Check for the presence of an auth token (this is just an example).
            // You might want to retrieve the token from Xamarin.Essentials SecureStorage or any other storage.
            var token = Xamarin.Essentials.SecureStorage.GetAsync("authtoken").Result;

            // Return true if a token exists, indicating that the user is logged in
            return !string.IsNullOrEmpty(token);
        }
        protected override void OnStart ()
        {
        }

        protected override void OnSleep ()
        {
        }

        protected override void OnResume ()
        {
        }
    }
}

