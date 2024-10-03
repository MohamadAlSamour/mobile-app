using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PrimeOption.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PrimeOption.ViewModels
{
    public class CalendarViewModel : BaseViewModel
    {
        private DateTime startOfWeek;
        public ObservableCollection<string> WeekDates { get; set; } // Holds the date strings for each day
        public ObservableCollection<Events> Events { get; set; } // Holds filtered events for the week

        private bool isBusy;
        public bool IsBusy
        {
            get => isBusy;
            set
            {
                isBusy = value;
                OnPropertyChanged(nameof(IsBusy));  // Notify UI when this changes
            }
        }

        public string CurrentWeekLabel { get; set; }
        public string WeekNumberLabel { get; set; }

        public Command PreviousWeekCommand { get; }
        public Command NextWeekCommand { get; }
        public Command CurrentWeekCommand { get; }

        // Store all events in memory after the first API call
        private List<Events> allEvents;

        public CalendarViewModel()
        {
            WeekDates = new ObservableCollection<string>();
            Events = new ObservableCollection<Events>();

            PreviousWeekCommand = new Command(() => GoToPreviousWeek());
            NextWeekCommand = new Command(() => GoToNextWeek());
            CurrentWeekCommand = new Command(() => GoToCurrentWeek());
            GoToCurrentWeek();
            InitAsync().ConfigureAwait(false);
        }

        private async Task InitAsync()
        {
            IsBusy = true;  // Start loading

            await FetchAllEvents();

            // After fetching data, set IsBusy to false
            IsBusy = false;
        }

        private void GoToCurrentWeek()
        {
            DateTime today = DateTime.Today;
            startOfWeek = StartOfWeek(today);
            UpdateWeekDates();
        }

        private void GoToPreviousWeek()
        {
            startOfWeek = startOfWeek.AddDays(-7); // Go back one week
            UpdateWeekDates();
        }

        private void GoToNextWeek()
        {
            startOfWeek = startOfWeek.AddDays(7); // Move forward one week
            UpdateWeekDates();
        }

        private void UpdateWeekDates()
        {
            WeekDates.Clear();
            Events.Clear(); // Clear existing events before filtering new ones

            // Update the week dates (Monday to Sunday)
            for (int i = 0; i < 7; i++)
            {
                var currentDay = startOfWeek.AddDays(i).ToString("dd MMM yyyy");
                WeekDates.Add(currentDay);
            }

            // Update labels
            CurrentWeekLabel = $"Week of {startOfWeek:dd MMM yyyy}";
            WeekNumberLabel = $"Week {GetIso8601WeekOfYear(startOfWeek)}";

            // Notify the UI
            OnPropertyChanged(nameof(WeekDates));
            OnPropertyChanged(nameof(CurrentWeekLabel));
            OnPropertyChanged(nameof(WeekNumberLabel));
            OnPropertyChanged(nameof(Events));

            if (allEvents != null)
            {
                FilterEventsForWeek(startOfWeek);
            }
        }

        // Filter events from the stored data for the current week
        private void FilterEventsForWeek(DateTime weekStart)
        {
            // Get the start and end dates for the current week
            var weekEnd = weekStart.AddDays(6);

            // Filter the events for the current week
            var filteredEvents = allEvents.Where(e => e.dateEvent >= weekStart && e.dateEvent <= weekEnd).ToList();

            foreach (var dayEvent in filteredEvents)
            {
                Events.Add(dayEvent); // Add filtered events to the observable collection
            }
        }

        // Fetch all events from an API
        private async Task FetchAllEvents()
        {
            try
            {
                // Example API URL - Replace with your actual API URL
                string apiUrl = "https://webapp-api-bfcvfte0b0ehabff.canadacentral-01.azurewebsites.net/api/user/profile";
                var httpClient = new HttpClient();

                // Retrieve the token from SecureStorage
                var token = await SecureStorage.GetAsync("auth_token");
                if (string.IsNullOrEmpty(token))
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Token not found. Please log in.", "OK");
                    return;
                }

                httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                // Fetch the data from the API
                var response = await httpClient.GetAsync(apiUrl);
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    await Application.Current.MainPage.DisplayAlert("Session Expired", "Please log in again.", "OK");
                    // Redirect to login page if necessary
                }

                if (response.IsSuccessStatusCode)
                {
                    // Read the response content
                    var responseContent = await response.Content.ReadAsStringAsync();

                    // Deserialize the response to a list of DayEvents (custom model)
                    var rootObject = JsonConvert.DeserializeObject<Root>(responseContent);
                    allEvents = rootObject.message;

                    // After fetching all events, filter them for the current week
                    FilterEventsForWeek(startOfWeek);
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Failed to fetch events: {ex.Message}", "OK");
            }
        }

        // Adjust the start of the week to be Monday
        private DateTime StartOfWeek(DateTime date)
        {
            // Calculate Monday as the start of the week
            var dayOfWeek = date.DayOfWeek == DayOfWeek.Sunday ? 7 : (int)date.DayOfWeek;
            int diff = dayOfWeek - (int)DayOfWeek.Monday;
            return date.AddDays(-diff).Date;
        }

        // Get ISO 8601 week number
        private int GetIso8601WeekOfYear(DateTime time)
        {
            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(time);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                time = time.AddDays(3);
            }

            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }
    }
}