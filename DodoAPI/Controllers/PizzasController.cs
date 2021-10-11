﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DodoAPI.Models;
using System.Diagnostics;
using Newtonsoft.Json;

namespace DodoAPI.Controllers
{
    [Route("api/pizzas")]
    [ApiController]
    public class PizzasController : ControllerBase
    {
        private readonly PizzaContext _context;
        /*public ActionResult TeamDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Pizza pizza = _context.Pizzas.Include(t => t.Ingredients).FirstOrDefault(t => t.id == id);
            if (pizza == null)
            {
                return NotFound();
            }
            return ;
        }*/
        public PizzasController(PizzaContext context)
        {
            _context = context;
        }

        // GET: api/Pizzas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pizza>>> GetPizzas()
        { 
            return await _context.Pizzas.Include(p => p.Ingredients).ToListAsync();
        }

        // GET: api/Pizzas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pizza>> GetPizza(long id)
        {
            var pizza = await _context.Pizzas.FindAsync(id);

            if (pizza == null)
            {
                return NotFound();
            }

            return pizza;
        }

        // PUT: api/Pizzas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPizza(long id, Pizza pizza)
        {
            if (id != pizza.id)
            {
                return BadRequest();
            }

            _context.Entry(pizza).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PizzaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Pizzas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Pizza>> PostPizza([FromBody]PizzaDto pizza)
        {
            var pizzaEntity = new Pizza()
            {
                Title = pizza.Title,
                Price = pizza.Price,
                Picture = pizza.Picture,
                Description = pizza.Description,
                Active = pizza.Active,
                New = pizza.New,
                //Ingredients = new List<Ingredient>(),
                Ingredients = new List<Ingredient>(),
                //Ingredients = _context.Ingredients,
                Dough = pizza.Dough,
                Additionally = pizza.Additionally,
            };
            foreach(var i in pizza.Ingredients)
            {
                var ingredient = new Ingredient() { Title = i };
                pizzaEntity.Ingredients.Add(ingredient);
            }
            _context.Pizzas.Add(pizzaEntity);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetPizza", new { id = pizza.id }, pizza);

            return new OkResult();
        }

        // DELETE: api/Pizzas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePizza(long id)
        {
            var pizza = await _context.Pizzas.FindAsync(id);
            if (pizza == null)
            {
                return NotFound();
            }

            _context.Pizzas.Remove(pizza);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PizzaExists(long id)
        {
            return _context.Pizzas.Any(e => e.id == id);
        }
    }
}
