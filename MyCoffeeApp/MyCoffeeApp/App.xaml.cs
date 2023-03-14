using MonkeyCache.FileStore;
using MyCoffeeApp.Helpers;
using MyCoffeeApp.Views;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyCoffeeApp
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            TheTheme.SetTheme();
            Barrel.ApplicationId = AppInfo.PackageName;
            //DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();//new NavigationPage(new ParentDashboard());

        }

        protected override void OnStart()
        {
            OnResume();
        }

        protected override void OnSleep()
        {
            TheTheme.SetTheme();
            RequestedThemeChanged -= App_RequestedThemeChanged;
        }

        protected override void OnResume()
        {
            TheTheme.SetTheme();
            RequestedThemeChanged += App_RequestedThemeChanged;
        }

        private void App_RequestedThemeChanged(object sender, AppThemeChangedEventArgs e)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                TheTheme.SetTheme();
            });
        }
    }
}
