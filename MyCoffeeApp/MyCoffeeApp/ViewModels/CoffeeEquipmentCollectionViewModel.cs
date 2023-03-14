using MvvmHelpers;
using MvvmHelpers.Commands;
using MyCoffeeApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MyCoffeeApp.ViewModels
{
    class CoffeeEquipmentCollectionViewModel : ViewModelBase
    {
        public ObservableRangeCollection<Coffee> Coffee { get; set; }
        public ObservableRangeCollection<Grouping<string, Coffee>> CoffeeGroups { get; set; }
        public AsyncCommand RefreshCommand { get; }
        public AsyncCommand<Coffee> FavoriteCommand { get; }
        public CoffeeEquipmentCollectionViewModel()
        {
            Title = "Coffee Equipment";
            Coffee = new ObservableRangeCollection<Coffee>();
            CoffeeGroups = new ObservableRangeCollection<Grouping<string, Coffee>>();
            var image = "https://www.yesplz.coffee/app/uploads/2020/11/emptybag-min.png";
            Coffee.Add(new Coffee { Roaster = "Yes Plz", Name = "Sip of sunshine", Image = image });
            Coffee.Add(new Coffee { Roaster = "Yes Plz", Name = "Potent potable", Image = image });
            Coffee.Add(new Coffee { Roaster = "Blue Bottle", Name = "Kenya Kiambu Hand", Image = image });
            Coffee.Add(new Coffee { Roaster = "Blue Bottle", Name = "Kenya Kiambu Hand", Image = image });
            Coffee.Add(new Coffee { Roaster = "Blue Bottle", Name = "Kenya Kiambu Hand", Image = image });
            Coffee.Add(new Coffee { Roaster = "Blue Bottle", Name = "Kenya Kiambu Hand", Image = image });
            Coffee.Add(new Coffee { Roaster = "Blue Bottle", Name = "Kenya Kiambu Hand", Image = image });

            CoffeeGroups.Add(new Grouping<string, Coffee>("Blue Bottle", new[] { Coffee[2] }));
            CoffeeGroups.Add(new Grouping<string, Coffee>("Yes Plz", Coffee.Take(2)));

            RefreshCommand = new AsyncCommand(Refresh);
            FavoriteCommand = new AsyncCommand<Coffee>(Favorite);
        }
        async Task Favorite(Coffee coffee)
        {
            if (coffee == null)
            {
                return;
            }
            await Application.Current.MainPage.DisplayAlert("Favorite", coffee.Name, "Ok");
        }
        Coffee previouslySelected;
        Coffee selectedCoffee;
        public Coffee SelectedCoffee
        {
            get => selectedCoffee;
            set
            {
                if (value != null)
                {
                    Application.Current.MainPage.DisplayAlert("Selected", value.Name, "Ok");
                    previouslySelected = value;
                    value = null;
                }
                selectedCoffee = value;
                OnPropertyChanged();
            }
        }
        async Task Refresh()
        {
            IsBusy = true;
            await Task.Delay(2000);
            IsBusy = false;
        }
        public ICommand CallServerCommand { get; }
        
        public ICommand IncreaseCount { get; }
        int count = 0;
        string countDisplay = "Click Me";
        public string CountDisplay
        {
            get => countDisplay;
            set => SetProperty(ref countDisplay, value);
        }
        void OnIncrease()
        {
            count++;
            CountDisplay = $"You clicked {count} times(s)";
        }
    }
}
