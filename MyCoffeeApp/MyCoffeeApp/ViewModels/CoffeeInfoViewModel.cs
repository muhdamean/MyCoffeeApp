using MyCoffeeApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace MyCoffeeApp.ViewModels
{
    public class CoffeeInfoViewModel
    {
        public ObservableCollection<Coffee> ItemList { get; set; }
        public CoffeeInfoViewModel()
        {
            ItemList = new ObservableCollection<Coffee>();
            var image = "https://www.yesplz.coffee/app/uploads/2020/11/emptybag-min.png";
            ItemList.Add(new Coffee { Id = 1, Name = "Computer", Roaster = "Computer Roaster", Image = image });
            ItemList.Add(new Coffee { Id = 2, Name = "Foods", Roaster = "Foods Roaster", Image = image });
            ItemList.Add(new Coffee { Id = 3, Name = "Electronics", Roaster = "Electronics Roaster", Image = image });
            ItemList.Add(new Coffee { Id = 4, Name = "Table", Roaster = "Table Roaster", Image = image });
        }
    }
}
