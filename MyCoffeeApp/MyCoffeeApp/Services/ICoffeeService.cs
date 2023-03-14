using MyCoffeeApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyCoffeeApp.Services
{
    public interface ICoffeeService 
    {
        Task AddCoffee(string name, string roaster);
        Task<IEnumerable<Coffee>> GetCoffee();
        Task<Coffee> GetCoffee(int id);
        Task RemoveCoffee(int id);
    }
}
