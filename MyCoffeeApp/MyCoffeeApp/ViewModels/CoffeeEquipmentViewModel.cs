using MvvmHelpers;
using MvvmHelpers.Commands;
using MyCoffeeApp.Models;
using MyCoffeeApp.Views.MyCoffee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MyCoffeeApp.ViewModels
{
    class CoffeeEquipmentViewModel : ViewModelBase //ObservableObject //BindableObject
    {
        //public ObservableRangeCollection<string> Coffee { get; }
        public ObservableRangeCollection<Coffee> Coffee { get; set; }
        public ObservableRangeCollection<Grouping<string, Coffee>> CoffeeGroups { get; set; }
        public AsyncCommand RefreshCommand { get; }
        public AsyncCommand<Coffee> FavoriteCommand { get; }
        public AsyncCommand<object> SelectedCommand { get; }
        public MvvmHelpers.Commands.Command LoadMoreCommand { get; }
        public MvvmHelpers.Commands.Command DelayLoadMoreCommand { get; }
        public MvvmHelpers.Commands.Command ClearCommand { get; }
        public CoffeeEquipmentViewModel()
        {
            //IncreaseCount = new Command(OnIncrease);
            //CallServerCommand = new AsyncCommand(CallServer);
            //Coffee = new ObservableRangeCollection<string>();
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

            CoffeeGroups.Add(new Grouping<string, Coffee>("Blue Bottle", new[] { Coffee[2]}));
            CoffeeGroups.Add(new Grouping<string, Coffee>("Yes Plz", Coffee.Take(2)));

            RefreshCommand = new AsyncCommand(Refresh);
            FavoriteCommand = new AsyncCommand<Coffee>(Favorite);
            SelectedCommand = new AsyncCommand<object>(Selected);

            //LoadMoreCommand = new MvvmHelpers.Commands.Command(LoadMore);
            //ClearCommand = new MvvmHelpers.Commands.Command(Clear);
            //DelayLoadMoreCommand = new MvvmHelpers.Commands.Command(DelayLoadMore);
        }
        async Task Favorite(Coffee coffee)
        {
            if (coffee==null)
            {
                return;
            }
            await Application.Current.MainPage.DisplayAlert("Favorite", coffee.Name, "Ok");
        }
        //Coffee previouslySelected;
        Coffee selectedCoffee;
        public Coffee SelectedCoffee
        {
            get => selectedCoffee;
            set
            {
                SetProperty(ref selectedCoffee, value);
                //if (value !=null)
                //{
                //    Application.Current.MainPage.DisplayAlert("Selected", value.Name, "Ok");
                //    previouslySelected = value;
                //    value = null;
                //}
                //selectedCoffee = value;
                //OnPropertyChanged();
            }
        }
        async Task Selected(object args)
        {
            var coffee = args as Coffee;
            if (coffee == null)
            {
                return;
            }
            SelectedCoffee = null;
            await AppShell.Current.GoToAsync(nameof(AddMyCoffeePage));
            //await Application.Current.MainPage.DisplayAlert("Selected", coffee.Name, "Ok");
        }
        async Task Refresh()
        {
            IsBusy = true;
            await Task.Delay(2000);
            IsBusy = false;
        }
        public ICommand CallServerCommand { get; }
        //async Task CallServer()
        //{
        //    var items = new List<string> { "Yes pls", "Tonx", "Blue Bottle" };
        //    //Coffee.AddRange(items);
        //}

        public ICommand IncreaseCount { get; }
        int count = 0;
        string countDisplay = "Click Me";
        public string CountDisplay
        {
            get => countDisplay;
            set => SetProperty(ref countDisplay, value);
            //get => countDisplay;
            //set
            //{
            //    if (value == countDisplay)
            //        return;
            //    countDisplay = value;
            //    OnPropertyChanged();
            //}
        }
        void OnIncrease()
        {
            count++;
            CountDisplay = $"You clicked {count} times(s)";
        }
    }

}
