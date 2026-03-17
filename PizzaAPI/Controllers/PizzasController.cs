using Microsoft.AspNetCore.Mvc;
using PizzaAPI.Models;

namespace PizzaAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PizzasController : ControllerBase
{
    // In-memory store for pizzas (for demonstration purposes)
    private static List<Pizza> pizzas = new List<Pizza>
    {
        new Pizza { Id = 1, Name = "Margherita", Price = 12.99m },
        new Pizza { Id = 2, Name = "Pepperoni", Price = 14.99m },
        new Pizza { Id = 3, Name = "Vegetarian", Price = 13.99m },
        new Pizza { Id = 4, Name = "Hawaiian", Price = 15.99m },
        new Pizza { Id = 5, Name = "Supreme", Price = 16.99m }
    };

    // GET: api/pizzas
    [HttpGet]
    public ActionResult<IEnumerable<Pizza>> GetPizzas()
    {
        return Ok(pizzas);
    }

    // GET: api/pizzas/{id}
    [HttpGet("{id}")]
    public ActionResult<Pizza> GetPizza(int id)
    {
        var pizza = pizzas.FirstOrDefault(p => p.Id == id);
        if (pizza == null)
        {
            return NotFound();
        }
        return Ok(pizza);
    }

    // POST: api/pizzas
    [HttpPost]
    public ActionResult<Pizza> CreatePizza(Pizza pizza)
    {
        if (pizza == null)
        {
            return BadRequest();
        }

        // Generate new ID
        pizza.Id = pizzas.Max(p => p.Id) + 1;
        pizzas.Add(pizza);

        return CreatedAtAction(nameof(GetPizza), new { id = pizza.Id }, pizza);
    }

    // PUT: api/pizzas/{id}
    [HttpPut("{id}")]
    public IActionResult UpdatePizza(int id, Pizza pizza)
    {
        var existingPizza = pizzas.FirstOrDefault(p => p.Id == id);
        if (existingPizza == null)
        {
            return NotFound();
        }

        existingPizza.Name = pizza.Name;
        existingPizza.Price = pizza.Price;

        return NoContent();
    }

    // DELETE: api/pizzas/{id}
    [HttpDelete("{id}")]
    public IActionResult DeletePizza(int id)
    {
        var pizza = pizzas.FirstOrDefault(p => p.Id == id);
        if (pizza == null)
        {
            return NotFound();
        }

        pizzas.Remove(pizza);
        return NoContent();
    }
}
