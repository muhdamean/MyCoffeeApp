﻿using MyCoffeeApp.Views;
using MyCoffeeApp.Views.MyCoffee;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace MyCoffeeApp
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(AddMyCoffeePage), typeof(AddMyCoffeePage));
            Routing.RegisterRoute(nameof(MyCoffeeDetailsPage), typeof(MyCoffeeDetailsPage));
            Routing.RegisterRoute(nameof(MyCoffeePage), typeof(MyCoffeePage));
            Routing.RegisterRoute(nameof(CoffeeEquipmentPage), typeof(CoffeeEquipmentPage));
        }
    }
}
