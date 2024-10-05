using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using PrimeOption.Models;
using PrimeOption.ViewModels;
using System.Linq;
using Xamarin.Essentials;

namespace PrimeOption.Views
{
    public partial class CalendarPage : ContentPage
    {
        public CalendarPage()
        {
            InitializeComponent();
            BindingContext = new CalendarViewModel();
        }


        //void OnReadMoreClicked(object sender, EventArgs e)
        //{
        //    // Get the button that was clicked
        //    var button = sender as Button;

        //    // Get the bound event data from the CommandParameter
        //    var selectedEvent = button?.CommandParameter as Events;

        //    if (selectedEvent != null)
        //    {
        //        // Display an alert with the event details and ask the user if they want to view the address in Google Maps
        //        Device.BeginInvokeOnMainThread(async () =>
        //        {
        //            bool showInMaps = await DisplayAlert($"{selectedEvent.title}",
        //                $"Day: {selectedEvent.dateEvent:dddd}\n" +
        //                $"Date: {selectedEvent.dateEvent:dd-MM-yyyy}\n" +
        //                $"Start: {selectedEvent.startTimeEvent:hh\\:mm}\n" +
        //                $"End: {selectedEvent.eindTimeEvent:hh\\:mm}\n" +
        //                $"Pause: {selectedEvent.pause:hh\\:mm}\n" +
        //                $"Project: {selectedEvent.requestName}\n" +
        //                $"Address: {selectedEvent.requestAdress}\n" +
        //                $"Remark: {selectedEvent.remarkforWorker}\n" +
        //                $"\nDo you want to view the address in Google Maps?",
        //                "Yes", "No");

        //            // If user clicked "Yes", open the address in Google Maps
        //            if (showInMaps)
        //            {
        //                OpenGoogleMaps(selectedEvent.requestAdress.ToString());
        //            }
        //        });
        //    }
        //}

        void OnReadMoreClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var selectedEvent = button?.CommandParameter as Events;

            if (selectedEvent != null)
            {
                var eventDetails = $"Day: {selectedEvent.dateEvent:dddd}\n" +
                                   $"Date: {selectedEvent.dateEvent:dd-MM-yyyy}\n" +
                                   $"Start: {selectedEvent.startTimeEvent:hh\\:mm}\n" +
                                   $"End: {selectedEvent.eindTimeEvent:hh\\:mm}\n" +
                                   $"Pause: {selectedEvent.pause:hh\\:mm}\n" +
                                   $"Project: {selectedEvent.requestName}\n" +
                                   $"Remark: {selectedEvent.remarkforWorker}\n";

                // Push the custom popup with event details
                Application.Current.MainPage.Navigation.PushModalAsync(new PopupPage(selectedEvent.title, eventDetails, selectedEvent.requestAdress.ToString()));
            }
        }



        private async void OpenGoogleMaps(string address)
        {
            var encodedAddress = Uri.EscapeDataString(address); // Encode the address for the URL
            var mapsUrl = $"https://www.google.com/maps/search/?api=1&query={encodedAddress}";

            // Open Google Maps
            await Launcher.OpenAsync(new Uri(mapsUrl));
        }

    }
}
