using Microsoft.EntityFrameworkCore;

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
        public string List_of_ingredients { get; set; }
        public string Dough { get; set; }
        public string Additionally { get; set; }
    }
}
