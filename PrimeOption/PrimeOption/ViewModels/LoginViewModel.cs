using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PrimeOption.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PrimeOption.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private string email;
        private string password;
        private bool isRememberMe;
        private bool isBusy; 

        public string Email
        {
            get => email;
            set => SetProperty(ref email, value);
        }

        public string Password
        {
            get => password;
            set => SetProperty(ref password, value);
        }

        public bool IsRememberMe
        {
            get => isRememberMe;
            set => SetProperty(ref isRememberMe, value);
        }

        public bool IsBusy
        {
            get => isBusy;
            set => SetProperty(ref isBusy, value);
        }

        public Command LoginCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new Command(async () => await ExecuteLoginCommand());

            // Load saved credentials if they exist
            LoadCredentials();
        }

        private async void LoadCredentials()
        {
            try
            {
                Email = await SecureStorage.GetAsync("saved_email");
                Password = await SecureStorage.GetAsync("saved_password");
                IsRememberMe = !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading credentials: {ex.Message}");
            }
        }

        private async Task ExecuteLoginCommand()
        {
            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
            {
                await Application.Current.MainPage.DisplayAlert("Oops...", "Please enter both email and password", "OK");
                return;
            }

            IsBusy = true;  // Start showing the loading indicator

            var loginSuccess = await LoginAsync(Email, Password);

            IsBusy = false; // Stop showing the loading indicator

            if (loginSuccess)
            {
                if (IsRememberMe)
                {
                    await SecureStorage.SetAsync("saved_email", Email);
                    await SecureStorage.SetAsync("saved_password", Password);
                }
                else
                {
                    SecureStorage.Remove("saved_email");
                    SecureStorage.Remove("saved_password");
                }

                Application.Current.MainPage = new NavigationPage(new CalendarPage());
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Oops...", "Login failed. Please try again.", "OK");
            }
        }

        private async Task<bool> LoginAsync(string username, string password)
        {
            try
            {
                var httpClient = new HttpClient();
                var url = "https://webapp-api-bfcvfte0b0ehabff.canadacentral-01.azurewebsites.net/api/jwt";

                var loginData = new
                {
                    Username = username,
                    Password = password
                };

                var json = JsonConvert.SerializeObject(loginData);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(responseContent);

                    await SecureStorage.SetAsync("auth_token", tokenResponse.Token);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Oops...", $"An error occurred: {ex.Message}", "OK");
                return false;
            }
        }

        public class TokenResponse
        {
            [JsonProperty("token")]
            public string Token { get; set; }
        }
    }
}




//using System;
//using System.Net.Http;
//using System.Text;
//using System.Threading.Tasks;
//using Newtonsoft.Json;
//using PrimeOption.Views;
//using Xamarin.Essentials;
//using Xamarin.Forms;

//namespace PrimeOption.ViewModels
//{
//    public class LoginViewModel : BaseViewModel
//    {
//        private string email;
//        private string password;

//        public string Email
//        {
//            get => email;
//            set => SetProperty(ref email, value);
//        }

//        public string Password
//        {
//            get => password;
//            set => SetProperty(ref password, value);
//        }

//        public Command LoginCommand { get; }

//        public LoginViewModel()
//        {
//            LoginCommand = new Command(async () => await ExecuteLoginCommand());
//        }

//        private async Task ExecuteLoginCommand()
//        {
//            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
//            {
//                await Application.Current.MainPage.DisplayAlert("Oops...", "Please enter both email and password", "OK");
//                return;
//            }

//            var loginSuccess = await LoginAsync(Email, Password);

//            if (loginSuccess)
//            {
//                //await Application.Current.MainPage.DisplayAlert("Success", "Logged in successfully!", "OK");
//                Application.Current.MainPage = new NavigationPage(new CalendarPage());
//            }
//            else
//            {
//                await Application.Current.MainPage.DisplayAlert("Oops...", "Login failed. Please try again.", "OK");
//            }
//        }

//        private async Task<bool> LoginAsync(string username, string password)
//        {
//            try
//            {
//                var httpClient = new HttpClient();
//                var url = "https://webapp-api-bfcvfte0b0ehabff.canadacentral-01.azurewebsites.net/api/jwt"; // Replace with your actual API URL

//                // Data to be sent in the login request
//                var loginData = new
//                {
//                    Username = username,
//                    Password = password
//                };

//                // Serialize login data to JSON
//                var json = JsonConvert.SerializeObject(loginData);
//                var content = new StringContent(json, Encoding.UTF8, "application/json");

//                // Send POST request
//                var response = await httpClient.PostAsync(url, content);

//                // Check if the response was successful
//                if (response.IsSuccessStatusCode)
//                {
//                    var responseContent = await response.Content.ReadAsStringAsync();
//                    var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(responseContent);

//                    // Save the token securely using Xamarin.Essentials SecureStorage
//                    await SecureStorage.SetAsync("auth_token", tokenResponse.Token);

//                    return true; // Login successful
//                }
//                else
//                {
//                    return false; // Login failed
//                }
//            }
//            catch (Exception ex)
//            {
//                await Application.Current.MainPage.DisplayAlert("Oops...", $"An error occurred: {ex.Message}", "OK");
//                return false;
//            }
//        }

//        // Model for deserializing the token response
//        public class TokenResponse
//        {
//            [JsonProperty("token")]
//            public string Token { get; set; }
//        }
//    }
//}
