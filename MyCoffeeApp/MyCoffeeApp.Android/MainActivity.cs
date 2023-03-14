﻿using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using MyCoffeeApp.Helpers;
using System.Drawing;
using Xamarin.Forms;
using Xamarin.Essentials;
using MyCoffeeApp.Services;
using Android.Widget;
using MyCoffeeApp.Droid;

[assembly:Dependency(typeof(Toaster))]
[assembly: Dependency(typeof(MyCoffeeApp.Droid.Environment))]
namespace MyCoffeeApp.Droid
{
    [Activity(Label = "MyCoffeeApp", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
    public class Environment : IEnvironment
    {
        public void SetStatusBarColor(System.Drawing.Color color, bool darkStatusBarTint)
        {
            if (Build.VERSION.SdkInt < Android.OS.BuildVersionCodes.Lollipop)
                return;

            var activity = Platform.CurrentActivity;
            var window = activity.Window;
            window.AddFlags(Android.Views.WindowManagerFlags.DrawsSystemBarBackgrounds);
            window.ClearFlags(Android.Views.WindowManagerFlags.TranslucentStatus);
            window.SetStatusBarColor(color.ToPlatformColor());

            if (Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.M)
            {
                var flag = (Android.Views.StatusBarVisibility)Android.Views.SystemUiFlags.LightStatusBar;
                window.DecorView.SystemUiVisibility = darkStatusBarTint ? flag : 0;
            }
        }
        
    }
    public class Toaster : IToast
    {
        public void MakeToast(string message)
        {
            Toast.MakeText(Platform.AppContext, message, ToastLength.Long).Show();
        }
    }
}