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
<<<<<<< HEAD
                optionsBuilder.UseSqlServer(@"Data Source=Istyuk-PC\SQLEXPRESS;Initial Catalog=PizzeriaDatabase2;Integrated Security=True;MultipleActiveResultSets=True;");
=======
                optionsBuilder.UseSqlServer(@"Data Source=Istyuk-PC\SQLEXPRESS;Initial Catalog=DatabasePizzeria;Integrated Security=True;MultipleActiveResultSets=True;");
>>>>>>> Lab5Base
            }
            base.OnConfiguring(optionsBuilder);
        }

        public virtual DbSet<Ingredient> Ingredients { set; get; }

        public virtual DbSet<Pizza> Pizzas { set; get; }

        public virtual DbSet<PizzaIngredient> PizzaIngredients { set; get; }

        public virtual DbSet<Order> Orders { set; get; }

<<<<<<< HEAD
        public virtual DbSet<Storage> Storages { set; get; }
        
        public virtual DbSet<StorageIngredient> StorageIngredients { set; get; }
=======
        public virtual DbSet<Client> Clients { set; get; }
>>>>>>> Lab5Base
    }
}
