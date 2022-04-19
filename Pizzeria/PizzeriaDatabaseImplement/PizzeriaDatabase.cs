using PizzeriaDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;

namespace PizzeriaDatabaseImplement
{
    public class PizzeriaDatabase : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer(@"Data Source=Istyuk-PC\SQLEXPRESS;Initial Catalog=DatabasePizzeria;Integrated Security=True;MultipleActiveResultSets=True;");
            }
            base.OnConfiguring(optionsBuilder);
        }

        public virtual DbSet<Ingredient> Ingredients { set; get; }

        public virtual DbSet<Pizza> Pizzas { set; get; }

        public virtual DbSet<PizzaIngredient> PizzaIngredients { set; get; }

        public virtual DbSet<Order> Orders { set; get; }

        public virtual DbSet<Client> Clients { set; get; }

        public virtual DbSet<Implementer> Implementers { set; get; }
    }
}
