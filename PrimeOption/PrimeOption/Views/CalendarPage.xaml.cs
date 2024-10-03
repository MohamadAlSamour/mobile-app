using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using PrimeOption.Models;
using PrimeOption.ViewModels;

namespace PrimeOption.Views
{
    public partial class CalendarPage : ContentPage
    {
        public CalendarPage()
        {
            InitializeComponent();
            BindingContext = new CalendarViewModel();
        }

        void SelectedEvent(System.Object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            var _event = e.SelectedItem as Events;
            DisplayAlert("Selected", $"{_event.title}\n {_event.dateEvent.ToString("dd-MM-yyyy")}\n {_event.startTimeEvent} - {_event.eindTimeEvent}\n ", "OK");
        }
    }
}
