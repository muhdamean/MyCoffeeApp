using System;
using System.Collections.Generic;
using System.Text;
using MyCoffeeApp.Models;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using Xamarin.Essentials;
using MonkeyCache.FileStore;
using System.Diagnostics;

namespace MyCoffeeApp.Services
{
    public static class InternetCoffeeService
    {
        //static string BaseUrl = DeviceInfo.Platform == DevicePlatform.Andriod ? "http://10.0.2.2:5000" : "http://localhost:5000";
        static string BaseUrl = "https://motzcoffee.azurewebsites.net";
       
        static HttpClient client;
        static InternetCoffeeService()
        {
            client = new HttpClient
            {
                BaseAddress = new Uri(BaseUrl)
            };
        }

        public static Task<IEnumerable<Coffee>> GetCoffee() =>
            GetAsync<IEnumerable<Coffee>>("api/Coffee", "getcoffee");
        //public static async Task<IEnumerable<Coffee>> GetCoffee()
        //{
        //    var json = await client.GetStringAsync("api/Coffee");
        //    var coffees = JsonConvert.DeserializeObject<IEnumerable<Coffee>>(json);
        //    return coffees;
        //}
        static async Task<T> GetAsync<T>(string url, string key, int mins = 1, bool forceRefresh = false)
        {
            var json = string.Empty;

            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
                json = Barrel.Current.Get<string>(key);
            else if (!forceRefresh && !Barrel.Current.IsExpired(key))
                json = Barrel.Current.Get<string>(key);

            try
            {
                if (string.IsNullOrWhiteSpace(json))
                {
                    json = await client.GetStringAsync(url);

                    Barrel.Current.Add(key, json, TimeSpan.FromMinutes(mins));
                }
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get information from server {ex}");
                throw ex;
            }
        }
        static Random random = new Random();
        public static async Task AddCoffee(string name, string roaster)
        {           
            var image = "https://www.yesplz.coffee/app/uploads/2020/11/emptybag-min.png";
            var coffee = new Coffee
            {
                Name = name,
                Roaster = roaster,
                Image = image,
                Id = random.Next(0,10000)
            };
            var json = JsonConvert.SerializeObject(coffee);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsJsonAsync("api/coffee", content);
            if (!response.IsSuccessStatusCode)
            {

            }
        }
       
        public static async Task RemoveCoffee(int id)
        {
           var response= await client.DeleteAsync($"api/coffee/{id}");
        }
        
        public static async Task<Coffee> GetCoffeeById(int id)
        {
            var response  = await client.GetStringAsync($"api/coffee/{id}");
            var coffee = JsonConvert.DeserializeObject<Coffee>(response);
            return coffee;
        }
    }
}
