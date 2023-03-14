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
    public partial class MyCoffeePage : ContentPage
    {
        public MyCoffeePage()
        {
            InitializeComponent();
            
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var vm = (MyCoffeeViewModel)BindingContext;
            if (vm.Coffees.Count == 0)
                await vm.RefreshCommand.ExecuteAsync();
        }
    }
}