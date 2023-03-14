using MyCoffeeApp.Models;
using MyCoffeeApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyCoffeeApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CoffeeEquipmentPage : ContentPage
    {
        public CoffeeEquipmentPage()
        {
            InitializeComponent();
            //BindingContext = new CoffeeEquipmentViewModel();
            //LabelCount.Text = "Hello from code Behind";
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var random = new Random();
            var random1 = (int)random.Next(0, 255);
            var random2 = (int)random.Next(0, 255);
            var random3 = (int)random.Next(0, 255);
            App.Current.Resources["WindowBackgroundColor"] = Color.FromRgb(random1, random2, random3);
        }

        //private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        //{
        //    var coffee = ((ListView)sender).SelectedItem as Coffee;
        //    if (coffee == null)
        //        return;

        //    await DisplayAlert("Coffee Selected", coffee.Name, "Ok");

        //}

        //private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        //{
        //    ((ListView)sender).SelectedItem = null;
        //}

        //private async void MenuItem_Clicked(object sender, EventArgs e)
        //{
        //    var coffee = ((MenuItem)sender).BindingContext as Coffee;
        //    if (coffee == null)
        //        return;

        //    await DisplayAlert("Coffee Favorited", coffee.Name, "Ok");
        //}



        //private void ButtonClick_Clicked(object sender, EventArgs e)
        //{
        //    count++;
        //    CountDisplay = $"You clicked {count} times(s)";
        //}
    }
}