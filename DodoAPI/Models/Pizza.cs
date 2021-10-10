using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DodoAPI.Models
{
    public class PizzaContext : DbContext
    {
        public PizzaContext(DbContextOptions<PizzaContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
    }
    public class Pizza
    {
        public long id { get; set; }
        public string Title { get; set; }
        public ushort Price { get; set; }
        public string Picture { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public bool New { get; set; }
        public ICollection<Ingredient> Ingredients { get; set; }
        public string Dough { get; set; }
        public string Additionally { get; set; }
    }
    public class Ingredient
    {
        public int id { get; set; }
        public string Title { get; set; }
        public int? PizzaId { get; set; }
        public virtual Pizza Pizza { get; set; }
    }
}
