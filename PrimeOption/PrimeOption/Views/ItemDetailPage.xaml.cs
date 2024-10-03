using System.ComponentModel;
using Xamarin.Forms;
using PrimeOption.ViewModels;

namespace PrimeOption.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}
