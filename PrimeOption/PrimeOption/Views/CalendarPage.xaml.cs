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

        void SelectedEvent(System.Object sender, SelectionChangedEventArgs e)
        {
            var _event = e.CurrentSelection.FirstOrDefault() as Events;

            if (_event != null)
            {
                DisplayAlert("Selected", $"{_event.title}\n {_event.dateEvent:dd-MM-yyyy}\n {_event.startTimeEvent} - {_event.eindTimeEvent}\n ", "OK");
            }
        }
    }
}
