using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using PrimeOption.Models;
using PrimeOption.ViewModels;
using System.Linq;

namespace PrimeOption.Views
{
    public partial class CalendarPage : ContentPage
    {
        public CalendarPage()
        {
            InitializeComponent();
            BindingContext = new CalendarViewModel();
        }


        // Event handler for "Read More" button click
        void OnReadMoreClicked(object sender, EventArgs e)
        {
            // Get the button that was clicked
            var button = sender as Button;

            // Get the bound event data from the CommandParameter
            var selectedEvent = button?.CommandParameter as Events;

            if (selectedEvent != null)
            {
                // Display an alert with the event details
                DisplayAlert($"{selectedEvent.title}",
                    $"Day: {selectedEvent.dateEvent:dddd}\n" +
                    $"Date: {selectedEvent.dateEvent:dd-MM-yyyy}\n" +
                    $"Start: {selectedEvent.startTimeEvent:hh\\:mm}\n" +
                    $"End: {selectedEvent.eindTimeEvent:hh\\:mm}\n" +
                    $"Pause: {selectedEvent.pause:hh\\:mm}\n" +
                    $"Project: {selectedEvent.requestName}\n" +
                    $"Adress: {selectedEvent.requestAdress}\n" +
                    $"Remark: {selectedEvent.remarkforWorker}\n",
                    "OK");
            }
        }

    }
}
