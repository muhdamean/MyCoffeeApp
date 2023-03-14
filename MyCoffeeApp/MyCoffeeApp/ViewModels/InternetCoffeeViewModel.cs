using MvvmHelpers;
using MvvmHelpers.Commands;
using MyCoffeeApp.Models;
using MyCoffeeApp.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyCoffeeApp.ViewModels
{
    public class InternetCoffeeViewModel : ViewModelBase
    {
        public ObservableRangeCollection<Coffee> Coffee { get; set; }
        public AsyncCommand RefreshCommand { get; }
        public AsyncCommand AddCommand { get; }
        public AsyncCommand<Coffee> RemoveCommand { get; }
       // public InternetCoffeeService InternetCoffeeService
        public InternetCoffeeViewModel()
        {
            Title = "My Coffee";
            Coffee = new ObservableRangeCollection<Coffee>();

            RefreshCommand = new AsyncCommand(Refresh);
            AddCommand = new AsyncCommand(Add);
            RemoveCommand = new AsyncCommand<Coffee>(Remove);
           
        }
        async Task Add()
        {
            var name = await App.Current.MainPage.DisplayPromptAsync("Name", "New Name");
            var roaster = await App.Current.MainPage.DisplayPromptAsync("Roaster", "New Roaster");
            await InternetCoffeeService.AddCoffee(name, roaster);
            await Refresh();
        }
        async Task Remove(Coffee coffee)
        {
            await InternetCoffeeService.RemoveCoffee(coffee.Id);
            await Refresh();
        }
        async Task Refresh()
        {
            IsBusy = true;
            await Task.Delay(2000);
            Coffee.Clear();
            var coffees = await InternetCoffeeService.GetCoffee();
            Coffee.AddRange(coffees);
            IsBusy = false;
        }
    }
}
