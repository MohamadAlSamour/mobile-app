using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using PrimeOption.Models;
using PrimeOption.ViewModels;
using System.Linq;
using Xamarin.Essentials;
using System.Threading.Tasks;

namespace PrimeOption.Views
{
    public partial class PopupPage : ContentPage
    {
        public PopupPage(string title, string details, string address)
        {
            InitializeComponent();
            BindingContext = new PopupPageViewModel(title, details, address);
        }
    }
}
