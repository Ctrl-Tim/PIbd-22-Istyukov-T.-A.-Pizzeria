using System.Collections.Generic;
using PizzeriaListImplement.Models;

namespace PizzeriaListImplement
{
    public class DataListSingleton
    {
        private static DataListSingleton instance;

        public List<Ingredient> Ingredients { get; set; }

        public List<Order> Orders { get; set; }

        public List<Pizza> Pizzas { get; set; }

<<<<<<< HEAD
        public List<Storage> Storages { get; set; }
=======
        public List<Client> Clients { get; set; }
>>>>>>> Lab5Base

        private DataListSingleton()
        {
            Ingredients = new List<Ingredient>();
            Orders = new List<Order>();
            Pizzas = new List<Pizza>();
<<<<<<< HEAD
            Storages = new List<Storage>();
=======
            Clients = new List<Client>();
>>>>>>> Lab5Base
        }

        public static DataListSingleton GetInstance()
        {
            if (instance == null)
            {
                instance = new DataListSingleton();
            }

            return instance;
        }
    }
}
