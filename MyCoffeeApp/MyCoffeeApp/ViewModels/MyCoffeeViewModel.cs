using MvvmHelpers;
using MvvmHelpers.Commands;
using MyCoffeeApp.Models;
using MyCoffeeApp.Services;
using MyCoffeeApp.Views;
using MyCoffeeApp.Views.MyCoffee;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
//using Command = MvvmHelpers.Commands.Command;

namespace MyCoffeeApp.ViewModels
{
    class MyCoffeeViewModel : ViewModelBase
    {
        public ObservableRangeCollection<Coffee> Coffees { get; set; }
        public AsyncCommand RefreshCommand { get; }
        public AsyncCommand AddCommand { get; }
        public AsyncCommand<Coffee> RemoveCommand { get; }
        public AsyncCommand<Coffee> SelectedCommand { get; }
        ICoffeeService coffeeService { get; }
        public MyCoffeeViewModel()
        {
            Title = "My Coffee";
            Coffees = new ObservableRangeCollection<Coffee>();
            RefreshCommand = new AsyncCommand(Refresh);
            AddCommand = new AsyncCommand(Add);
            RemoveCommand = new AsyncCommand<Coffee>(Remove);
            SelectedCommand = new AsyncCommand<Coffee>(Selected);
            coffeeService = DependencyService.Get<ICoffeeService>();
        }
        
        
        async Task Add()
        {
            //var name = await App.Current.MainPage.DisplayPromptAsync("Name", "Name of coffee");
            //var roaster = await App.Current.MainPage.DisplayPromptAsync("Roaster", "Roaster of coffee");
            //await coffeeService.AddCoffee(name, roaster);
            //await Refresh();

            var route = $"{nameof(AddMyCoffeePage)}?Name=Motz";
            await Shell.Current.GoToAsync(route);
        }
        Coffee selectedCoffee;
        public Coffee SelectedCoffee
        {
            get => selectedCoffee;
            set
            {
                SetProperty(ref selectedCoffee, value);
            }
        }

        async Task Selected(Coffee coffee)
        {
            if (coffee==null)
            {
                return;
            }
            var route = $"{nameof(MyCoffeeDetailsPage)}?CoffeeId={coffee.Id}";
            await Shell.Current.GoToAsync(route);
            //await Application.Current.MainPage.DisplayAlert("Selected", coffee.Name, "Ok");
        }
        async Task Remove(Coffee coffee)
        {
            await coffeeService.RemoveCoffee(coffee.Id);
            await Refresh();
        }
        async Task Refresh()
        {
            IsBusy = true;
            await Task.Delay(2000);
            Coffees.Clear();
            var coffees =await coffeeService.GetCoffee();
            Coffees.AddRange(coffees);
            IsBusy = false;
            DependencyService.Get<IToast>()?.MakeToast("Refreshed");
        }
    }
}
