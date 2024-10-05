using System;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PrimeOption.ViewModels
{
    public class PopupPageViewModel : BaseViewModel
    {
        public string EventTitle { get; set; }
        public string EventDetails { get; set; }

        public Command OpenMapsCommand { get; }
        public Command CloseCommand { get; }

        private string _address;

        public PopupPageViewModel(string title, string details, string address)
        {
            EventTitle = title;
            EventDetails = details;
            _address = address;

            OpenMapsCommand = new Command(async () => await OpenGoogleMaps());
            CloseCommand = new Command(async () => await ClosePopup());
        }

        private async Task OpenGoogleMaps()
        {
            if (!string.IsNullOrWhiteSpace(_address))
            {
                var encodedAddress = Uri.EscapeDataString(_address);
                var mapsUrl = $"https://www.google.com/maps/search/?api=1&query={encodedAddress}";
                await Launcher.OpenAsync(new Uri(mapsUrl));
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Address not available.", "OK");
            }
        }

        private async Task ClosePopup()
        {
            await Application.Current.MainPage.Navigation.PopModalAsync();
        }
    }
}
