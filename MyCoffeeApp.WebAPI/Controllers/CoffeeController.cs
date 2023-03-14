using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyCoffeeApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCoffeeApp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoffeeController : ControllerBase
    {
        public static List<Coffee> Coffees { get; } = new List<Coffee>();
        [HttpGet]
        public IEnumerable<Coffee> Get()
        {
            return Coffees;
        }
        [HttpGet("{id}")]
        public ActionResult<Coffee> GetById(int id)
        {
            var coffee = Coffees.FirstOrDefault(x => x.Id == id);
            if (coffee==null)
            {
                return BadRequest();
            }
            return coffee;
        }
        [HttpPost]
        public void Post([FromBody] Coffee coffee)
        {
            Coffees.Add(coffee);
        }
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Coffee coffee)
        {
            var coffeeValue = Coffees.FirstOrDefault(x => x.Id == id);
            if (coffeeValue==null)
            {
                return;
            }
            coffeeValue = coffee;
        }
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var coffee = Coffees.FirstOrDefault(x => x.Id == id);
            if (coffee==null)
            {
                return;
            }
            Coffees.Remove(coffee);
        }
    }
}
