using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DodoAPI.Models
{
    public class PizzaContext : DbContext
    {
        public PizzaContext(DbContextOptions<PizzaContext> options)
            : base(options)
        {
        }

        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
    }
    [Table("Pizza")]
    public class Pizza
    {
       
        [Required]
        [Key]
        public long id { get; set; }
        public string Title { get; set; }
        public ushort? Price { get; set; }
        public string Picture { get; set; }
        public string Description { get; set; }
        public bool? Active { get; set; }
        public bool? New { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public string Dough { get; set; }
        public string Additionally { get; set; }
    }
    public class Ingredient
    {
        [Required]
        [Key]
        public int id { get; set; }
        public string Title { get; set; }
        [ForeignKey("Pizza")]
        public long? PizzaId { get; set; }
        public virtual Pizza Pizza { get; set; }
    }
}
